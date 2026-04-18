using Backend.DTOs.Internals;

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
		}

		await next(context);
	}
}