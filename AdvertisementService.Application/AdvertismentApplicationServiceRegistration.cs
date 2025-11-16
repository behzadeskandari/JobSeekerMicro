using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdvertisementService.Application
{
    public static class AdvertismentApplicationServiceRegistration
    {
        public static IServiceCollection AdvertismentApplicationService(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole().SetMinimumLevel(LogLevel.Debug);
                builder.AddDebug();

            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(AdvertismentApplicationServiceRegistration).GetTypeInfo().Assembly);
            });
            services.AddValidatorsFromAssembly(typeof(AdvertismentApplicationServiceRegistration).GetTypeInfo().Assembly);
            services.AddFluentValidationAutoValidation();
            return services;
        }
    }
}
