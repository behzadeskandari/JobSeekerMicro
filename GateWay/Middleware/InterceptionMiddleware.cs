namespace GateWay.Middleware
{
    public class InterceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.Headers["X-Intercepted"] = "true";
            context.Request.Headers["referrer"] = "true";
            await next(context);
        }
    }
}
