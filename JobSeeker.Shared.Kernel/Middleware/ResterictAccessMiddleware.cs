using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
namespace JobSeeker.Shared.Kernel.Middleware
{
    public class ResterictAccessMiddleware
    {
        private readonly RequestDelegate _next;
        public ResterictAccessMiddleware(RequestDelegate next) => _next = next;
        public async Task InvokeAsync(HttpContext context)
        {
            // Example logic: Restrict access based on a custom header
            if (context.Request.Headers["X-Intercepted"] != "true")
            {
                var referrer = context.Request.Headers["referer"].ToString();
                if (string.IsNullOrEmpty(referrer))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Access Forbidden");
                    return;
                }
                else
                {
                    await context.Response.WriteAsync($"Access Allowed from referrer: {referrer}");
                    await _next(context);
                }
            }
            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
