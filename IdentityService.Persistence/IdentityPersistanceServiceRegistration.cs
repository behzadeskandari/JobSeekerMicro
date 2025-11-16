using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Services;
using IdentityService.Persistence.GenericRepository;
using IdentityService.Persistence.Repository;
using JobSeeker.Shared.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Persistence
{
    public static class IdentityPersistanceServiceRegistration
    {
        public static IServiceCollection AddIdentityPersistanceServiceRegistration(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped(typeof(IWriteRepository<>), typeof(GenericWriteRepository<>));
            services.AddScoped<IIdentityUnitOfWOrk, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }
    }
}
