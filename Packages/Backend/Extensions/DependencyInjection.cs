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
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
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
					var accessToken = context.Request.Cookies["access"];

					var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerHandler>>();
					logger.LogInformation("--- CHECKING COOKIE: {Token} ---", accessToken ?? "NULL");

					context.Token = accessToken;

					return Task.CompletedTask;
				},
				OnAuthenticationFailed = context =>
				{
					// 🚩 ĐẶT DEBUG TẠI ĐÂY: Nếu Token sai, hết hạn, hoặc lệch Key, nó sẽ nhảy vào đây.
					// Phát xem biến 'context.Exception' để biết lý do chính xác (ví dụ: SecurityTokenExpiredException)
					return Task.CompletedTask;
				},
				OnTokenValidated = context =>
				{
					// 🚩 ĐẶT DEBUG TẠI ĐÂY: Nếu nó nhảy vào đây là Token HỢP LỆ. 
					// Bạn có thể xem 'context.Principal' để thấy các Claims.
					return Task.CompletedTask;
				}
			};
		});

		return services;
	}
}