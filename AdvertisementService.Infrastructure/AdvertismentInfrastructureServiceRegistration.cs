using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertisementService.Infrastructure
{
    public static class AdvertismentInfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureAdvertismentInfrastructureServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }
    }
}
