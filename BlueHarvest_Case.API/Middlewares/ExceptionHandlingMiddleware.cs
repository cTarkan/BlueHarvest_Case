using FluentValidation;
using Newtonsoft.Json;
using System.Net;

namespace BlueHarvest_Case.API.Middlewares
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (ValidationException ex) // FluentValidation Hatalarını Yakala
			{
				await HandleValidationExceptionAsync(context, ex);
			}
			catch (KeyNotFoundException ex)
			{
				await HandleKeyNotFoundExceptionAsync(context, ex);
			}
			catch (Exception ex)
			{
				await HandleGenericExceptionAsync(context, ex);
			}
		}

		private Task HandleKeyNotFoundExceptionAsync(HttpContext context, KeyNotFoundException exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = StatusCodes.Status404NotFound;
			return context.Response.WriteAsJsonAsync(new { Message = exception.Message });
		}

		private Task HandleGenericExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;

			var errorDetails = new
			{
				Message = "An unexpected error occurred",
				Error = exception.Message
			};

			return context.Response.WriteAsJsonAsync(errorDetails);
		}

		private Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = StatusCodes.Status400BadRequest;

			var errorDetails = new
			{
				Message = "Validation failed",
				Errors = exception.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
			};

			return context.Response.WriteAsJsonAsync(errorDetails);
		}

	}
}
