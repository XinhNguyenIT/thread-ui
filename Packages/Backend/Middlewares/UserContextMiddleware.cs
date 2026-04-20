using Backend.DTOs.Internals;
using Backend.Enums;

namespace Backend.Middlewares;

public class UserContextMiddleware(RequestDelegate next)
{
	public async Task InvokeAsync(HttpContext context, UserContext userContext)
	{
		if (context.User.Identity?.IsAuthenticated == true)
		{
			userContext.UserId = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
								 ?? context.User.FindFirst("sub")?.Value
								 ?? string.Empty;

			userContext.Email = context.User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value
								?? string.Empty;

			userContext.Roles = context.User.FindAll(System.Security.Claims.ClaimTypes.Role)
									.Select(c => Enum.Parse<RoleTypeEnum>(c.Value))
									.ToList();
		}

		await next(context);
	}
}