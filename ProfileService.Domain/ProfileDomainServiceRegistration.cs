using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Domain
{
    public static  class ProfileDomainServiceRegistration
    {
        public static IServiceCollection ConfigureProfileDomainServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }
    }
}
