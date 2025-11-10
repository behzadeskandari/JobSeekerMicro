using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IdentityService.Api.Filters
{
    public class SwaggerExcludeAuthOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAllowAnonymous = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AllowAnonymousAttribute>()
                .Any();

            if (hasAllowAnonymous)
            {
                operation.Security.Clear(); // Remove Bearer auth requirement for this endpoint
            }
        }
    }
}
