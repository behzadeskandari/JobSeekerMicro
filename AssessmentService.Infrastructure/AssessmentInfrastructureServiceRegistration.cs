using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Infrastructure
{
    public static class AssessmentInfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureAssessmentInfrastructureServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
