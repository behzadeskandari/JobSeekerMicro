using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Domain
{
    public static class AssessmentDomainServiceRegistration
    {
        public static IServiceCollection ConfigureAssessmentDomainServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }
    }
}
