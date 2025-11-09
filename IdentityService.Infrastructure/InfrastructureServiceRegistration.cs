using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Infrastructure.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
        public static IServiceCollection AddJWTService(this IServiceCollection services, IConfiguration config) {

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                { 
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    //ValidateLifetime =  ️true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["JWT:Issuer"],
                    ValidAudience = config["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]))

                };
            });
            return services;
        }
    }
}
