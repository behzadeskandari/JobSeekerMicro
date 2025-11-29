using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Domain
{
    public static class IdentityDomainServiceRegistration
    {
        public static IServiceCollection ConfigureIdentityDomainServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }
    }
}
