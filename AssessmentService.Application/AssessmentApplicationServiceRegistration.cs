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

namespace AssessmentService.Application
{
    public static class AssessmentApplicationServiceRegistration
    {
        public static IServiceCollection AssessmentApplicationService(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole().SetMinimumLevel(LogLevel.Debug);
                builder.AddDebug();

            });

            services.AddAutoMapper(x => Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(AssessmentApplicationServiceRegistration).GetTypeInfo().Assembly);
            });
            services.AddValidatorsFromAssembly(typeof(AssessmentApplicationServiceRegistration).GetTypeInfo().Assembly);
            services.AddFluentValidationAutoValidation();
            return services;
        }
    }
}
