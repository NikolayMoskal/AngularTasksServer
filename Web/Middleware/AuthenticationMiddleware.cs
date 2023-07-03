using MediaItemsServer.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;

namespace MediaItemsServer.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IList<string> _allowedRoles;

        public AuthenticationMiddleware(RequestDelegate next, IList<string> allowedRoles = null)
        {
            _next = next;
            _allowedRoles = allowedRoles ?? new List<string>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            // skip validation for authentication requests
            if (httpContext.Request.Path.StartsWithSegments(RouteHelper.ConvertToRoute(Consts.AuthRoute)))
            {
                await _next(httpContext);
                return;
            }

            var authHeader = httpContext.Request.Headers[HeaderNames.Authorization].ToString();
            if (string.IsNullOrEmpty(authHeader))
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var jwt = authHeader.Split(' '); // format: Bearer <JWT>
            var schema = jwt[0];
            if (schema != JwtBearerDefaults.AuthenticationScheme)
            {
                WriteWithStatusAsync(httpContext.Response, "Incorrect JWT Bearer schema", StatusCodes.Status400BadRequest);
            }

            var token = jwt[1];
            if (JwtTokenHelper.IsTokenExpired(token))
            {
                WriteWithStatusAsync(httpContext.Response, "Token has expired. Try to refresh.", StatusCodes.Status401Unauthorized);
                return;
            }

            var claims = JwtTokenHelper.GetPrincipal(token).Claims;

            // only given roles are allowed
            if (claims.Any(x => _allowedRoles.Contains(x.Value)))
            {
                await _next(httpContext);
                return;
            }

            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
        }

        private static async void WriteWithStatusAsync(HttpResponse httpResponse, string text, int statusCode = 200)
        {
            httpResponse.StatusCode = statusCode;
            await httpResponse.WriteAsync(text);
        }
    }

    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthentication(this IApplicationBuilder app, IList<string> allowedRoles)
        {
            app.UseMiddleware<AuthenticationMiddleware>(allowedRoles);
            
            return app;
        }
    }
}
