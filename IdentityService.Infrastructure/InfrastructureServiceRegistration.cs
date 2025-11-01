using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Infrastructure.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrastructureRegistrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuth();

            return services;
        }
        private static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddSingleton<IJwtTokenGenerator, JwtService>();

            return services;
        }
    }


    public static class JWTAuthService{
        public static IServiceCollection AddJWTService(this IServiceCollection services) {

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBrarerDefault
            });
            return services;
        }
    }
}
