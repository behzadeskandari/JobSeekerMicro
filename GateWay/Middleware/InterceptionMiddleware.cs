namespace GateWay.Middleware
{
    public class InterceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public InterceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.Headers["X-Intercepted"] = "true";
            context.Request.Headers["Referer"] = "trusted-gateway"; // optional

            await _next(context);
        }
    }
    //public class InterceptionMiddleware(RequestDelegate next)
    //{
    //    private readonly RequestDelegate _next;
    //    public async Task InvokeAsync(HttpContext context)
    //    {
    //        context.Request.Headers["X-Intercepted"] = "true";
    //        context.Request.Headers["Referer"] = "trusted-gateway"; // optional

    //        await _next(context);
    //    }
    //}
}
