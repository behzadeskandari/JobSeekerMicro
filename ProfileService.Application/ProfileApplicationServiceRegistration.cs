using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Application
{
    public static class ProfileApplicationServiceRegistration
    {
        public static IServiceCollection ProfileApplicationService(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole().SetMinimumLevel(LogLevel.Debug);
                builder.AddDebug();

            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(ProfileApplicationServiceRegistration).GetTypeInfo().Assembly);
            });
            services.AddValidatorsFromAssembly(typeof(ProfileApplicationServiceRegistration).GetTypeInfo().Assembly);
            services.AddFluentValidationAutoValidation();
            return services;
        }
    }
}
