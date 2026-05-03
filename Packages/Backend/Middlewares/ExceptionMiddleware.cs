using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Backend.Exceptions;

namespace Backend.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await handleExceptionAsync(context, ex);
			}
		}

		private async Task handleExceptionAsync(HttpContext context, Exception ex)
		{
			_logger.LogError(ex, ex.Message);

			context.Response.ContentType = "application/json";

			var statusCode = HttpStatusCode.InternalServerError;
			var message = "";
			var errors = new List<string>();

			switch (ex)
			{
				case ValidationException validationEx:
					statusCode = HttpStatusCode.BadRequest;
					message = validationEx.Message;
					errors = validationEx.Errors;
					break;

				case BadHttpRequestException:
					statusCode = HttpStatusCode.BadRequest;
					message = ex.Message;
					break;

				case UnauthorizedAccessException:
					statusCode = HttpStatusCode.Unauthorized;
					message = ex.Message;
					break;

				case KeyNotFoundException:
					statusCode = HttpStatusCode.NotFound;
					message = ex.Message;
					break;

				case ArgumentException:
					statusCode = HttpStatusCode.BadRequest;
					message = ex.Message;
					break;

				default:
					message = ex.Message;
					break;
			}

			context.Response.StatusCode = (int)statusCode;

			var response = new Dictionary<string, object?>
			{
				["success"] = false,
				["statusCode"] = context.Response.StatusCode,
				["message"] = message,
				["traceId"] = context.TraceIdentifier
			};

			if (errors?.Any() == true)
			{
				response["errors"] = errors;
			}

			var options = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				WriteIndented = true
			};

			await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
		}
	}
}