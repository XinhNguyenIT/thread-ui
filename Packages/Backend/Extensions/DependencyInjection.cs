using System.Text;
using Backend.DTOs.Internals;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Extensions;

public static class DependencyInjection
{
	public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		var jwt = configuration.GetSection("Jwt").Get<JwtSettings>();
		var key = Encoding.UTF8.GetBytes(jwt!.Key);

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwt.Issuer,
				ValidAudience = jwt.Audience,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ClockSkew = TimeSpan.Zero
			};

			options.Events = new JwtBearerEvents
			{
				OnMessageReceived = context =>
				{
					var accessToken = context.Request.Cookies["accessToken"];
					if (!string.IsNullOrEmpty(accessToken))
					{
						context.Token = accessToken;
					}
					return Task.CompletedTask;
				}
			};
		});

		return services;
	}
}