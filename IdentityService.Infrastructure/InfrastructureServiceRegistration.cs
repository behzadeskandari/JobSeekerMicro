using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Infrastructure.Jwt;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;

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


    public static class JWTAuthService
    {
        public static IServiceCollection AddJWTService(this IServiceCollection services, IConfiguration config)
        {


            services.AddAuthentication(options =>
            {
                 options.DefaultAuthenticateScheme =
                 options.DefaultChallengeScheme =
                 options.DefaultForbidScheme =
                 options.DefaultScheme =
                 options.DefaultSignInScheme =
                 options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Bearer", opt =>
            {
                opt.Authority = config["Identity:Authority"];
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
                opt.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication failed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated successfully.");
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        return Task.CompletedTask;
                    }
                };
                services.AddSingleton(opt.TokenValidationParameters);
            });
            return services;
        }
    }
}
