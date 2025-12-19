using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Infrastructure
{
    public static class ProfileInfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureProfileInfrastructureServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }
    }
}
