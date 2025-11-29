using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertisementService.Domain
{
    public static class AdvertismentDomainServiceRegistration
    {
        public static IServiceCollection ConfigureAdvertismentDomainServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }

    }
}
