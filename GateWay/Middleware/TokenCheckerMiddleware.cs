using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace GateWay.Middleware
{
    public class TokenCheckerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public TokenCheckerMiddleware(RequestDelegate next, TokenValidationParameters tokenValidationParameters)
        {
            _next = next;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Skip middleware for login/register endpoints
            var path = context.Request.Path.Value?.ToLower();

            if (path.StartsWith("/account/account/register") || path.StartsWith("/account/account/login"))
            {
                await _next(context);
                return;
            }
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                try
                {
                    handler.ValidateToken(token, _tokenValidationParameters, out var _);
                }
                catch (Exception)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid or expired token");
                    return;
                }
            }

            await _next(context);
        }
    }

}
