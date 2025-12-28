using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Application
{
    public static class JobApplicationServiceRegistration
    {
        public static IServiceCollection AddJobApplicationServiceRegistration(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole().SetMinimumLevel(LogLevel.Debug);
                builder.AddDebug();

            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(JobApplicationServiceRegistration).GetTypeInfo().Assembly);
            });
            services.AddValidatorsFromAssembly(typeof(JobApplicationServiceRegistration).GetTypeInfo().Assembly);
            services.AddFluentValidationAutoValidation();


            return services;
        }
    }
}
}
