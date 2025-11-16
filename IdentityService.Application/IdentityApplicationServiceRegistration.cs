using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityService.Application
{
    internal static class IdentityApplicationServiceRegistration
    {
        public static IServiceCollection AddIdentityApplicationServiceRegistration(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole().SetMinimumLevel(LogLevel.Debug);
                builder.AddDebug();

            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(IdentityApplicationServiceRegistration).GetTypeInfo().Assembly);
            });
            services.AddValidatorsFromAssembly(typeof(IdentityApplicationServiceRegistration).GetTypeInfo().Assembly);
            services.AddFluentValidationAutoValidation();


            return services;
        }
    }
}
