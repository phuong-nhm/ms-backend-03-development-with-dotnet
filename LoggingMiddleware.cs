namespace UserManagementAPI
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log incoming request
            Console.WriteLine($"Incoming Request: {context.Request.Method} {context.Request.Path}");

            // Call the next middleware in the pipeline
            await _next(context);

            // Log outgoing response
            Console.WriteLine($"Outgoing Response: {context.Response.StatusCode}");

        }
    }
}