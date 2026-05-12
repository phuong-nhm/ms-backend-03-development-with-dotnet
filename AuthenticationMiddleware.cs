namespace UserManagementAPI
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check for Authorization header
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("{\"error\":\"Missing Authorization header.\"}");
                return;
            }

            var token = context.Request.Headers["Authorization"].ToString();

            // Simple validation: check if token equals a predefined value
            // In real-world, you would validate JWT or another token type
            if (string.IsNullOrEmpty(token) || !ValidateToken(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("{\"error\":\"Invalid or expired token.\"}");
                return;
            }

            // Token is valid → continue pipeline
            await _next(context);
        }

        private bool ValidateToken(string token)
        {
            // Example: expecting "Bearer mysecrettoken"
            return token == "Bearer mysecrettoken";
        }
    }
}
