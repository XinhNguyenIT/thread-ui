using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Backend.DTOs.Internals;
using Backend.Enums;
using Backend.Helpers;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services;

public class JwtService : IJwtService
{
	private readonly IConfiguration _config;
	private readonly IUnitOfWork _unitOfWork;

	public JwtService(IConfiguration config, IUnitOfWork unitOfWork)
	{
		_config = config;
		_unitOfWork = unitOfWork;
	}

	public async Task<List<TokenReturn>> CreateTokenForUser(User user, List<RoleTypeEnum> roles)
	{
		var tokenNames = new List<string> { "access", "refresh" };
		var results = new List<TokenReturn>();

		foreach (var tokenName in tokenNames)
		{
			var type = tokenName == "access" ? TokenTypeEnum.ACCESS : TokenTypeEnum.REFRESH;
			var token = GenerateToken(user.Id, user.Email, roles, type);
			results.Add(token);

			if (type == TokenTypeEnum.REFRESH)
			{
				var newRefreshToken = new RefreshToken
				{
					Token = token.Token,
					UserId = user.Id,
					ExpiryDate = token.Expires,
				};

				await _unitOfWork.BeginTransactionAsync();

				try
				{
					var notRevokedTokens = await _unitOfWork.RefreshTokenRepository.FindByUserId(user.Id);

					foreach (var notRevokedToken in notRevokedTokens)
					{
						await _unitOfWork.RefreshTokenRepository.RevokeToken(notRevokedToken.Id);
					}

					await _unitOfWork.RefreshTokenRepository.AddAsync(newRefreshToken);

					await _unitOfWork.CommitAsync();
				}
				catch (Exception)
				{
					await _unitOfWork.RollbackAsync();
					throw;
				}

			}
		}

		return results;
	}

	private TokenReturn GenerateToken(string userId, string? email, List<RoleTypeEnum> roles, TokenTypeEnum type)
	{
		var jwt = _config.GetSection("Jwt");
		var expires = type == TokenTypeEnum.ACCESS ?
					DateTime.UtcNow.AddMinutes(double.Parse(jwt["AccessTokenMinutes"]!)) :
					DateTime.UtcNow.AddDays(double.Parse(jwt["RefreshTokenDays"]!));

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, userId),
			new Claim(ClaimTypes.Email, email ?? "")
		};

		foreach (var role in roles)
		{
			claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
		}


		var token = new JwtSecurityToken(
			issuer: jwt["Issuer"],
			audience: jwt["Audience"],
			claims: claims,
			expires: expires,
			signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
		);

		var result = new TokenReturn
		{
			Token = new JwtSecurityTokenHandler().WriteToken(token),
			Expires = expires,
			Type = type,
		};

		return result;
	}

	public string RevokeToken(string userId, string token)
	{
		throw new NotImplementedException();
	}
}