using System.Net;
using System.Text.Json;

namespace UserManagementAPI
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Pass request down the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Handle any unhandled exception
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Always return JSON error response
            var response = new
            {
                error = "Internal server error.",
                details = exception.Message // optional: remove in production for security
            };

            var payload = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(payload);
        }
    }
}
