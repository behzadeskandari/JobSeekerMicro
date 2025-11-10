using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace GateWay.Middleware
{
    public class TokenCheckerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenValidationParameters _validationParameters;
        private readonly ILogger<TokenCheckerMiddleware> _logger;

        public TokenCheckerMiddleware(RequestDelegate next, TokenValidationParameters validationParameters, ILogger<TokenCheckerMiddleware> logger)
        {
            _next = next;
            _validationParameters = validationParameters;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            // allow health checks or swagger without token: skip if path matches
            var path = ctx.Request.Path.Value ?? string.Empty;
            if (path.StartsWith("/swagger") || path.StartsWith("/health") || path == "/")
            {
                await _next(ctx);
                return;
            }

            string authHeader = ctx.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                // If you want public endpoints, you may allow missing token. For protected endpoints, return 401
                // We forward without token only for endpoints configured as anonymous; else return 401
                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await ctx.Response.WriteAsync("Missing or invalid Authorization header");
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var principal = handler.ValidateToken(token, _validationParameters, out var validatedToken);

                // optional: enforce particular claim or scope presence
                if (!principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub"))
                {
                    ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await ctx.Response.WriteAsync("Token missing required subject claim");
                    return;
                }

                // Set a few short headers to flow identity to downstream services (Ocelot will forward headers)
                var sub = principal.FindFirst("sub")?.Value ?? principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(sub))
                {
                    ctx.Request.Headers["X-User-Id"] = sub;
                }

                // Optionally set ClaimsPrincipal for downstream controllers 
                ctx.User = principal;

                await _next(ctx);
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogWarning(ex, "Token validation failed");
                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await ctx.Response.WriteAsync("Token validation failed");
            }
        }
    }


}
