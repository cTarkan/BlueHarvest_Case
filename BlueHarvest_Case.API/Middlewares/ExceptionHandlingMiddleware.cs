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
			catch (Exception ex)
			{
				await HandleGenericExceptionAsync(context, ex);
			}
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
	}
}
