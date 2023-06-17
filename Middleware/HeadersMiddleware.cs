using Microsoft.Net.Http.Headers;

namespace MediaItemsServer.Middleware
{
    public class HeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public HeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Response.Headers[HeaderNames.AccessControlAllowOrigin] = "*";
            httpContext.Response.Headers[HeaderNames.AccessControlAllowHeaders] = "*";
            httpContext.Response.Headers[HeaderNames.AccessControlAllowMethods] = "GET, POST, PUT, DELETE, OPTIONS";
            
            return _next(httpContext);
        }
    }
}
