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
using JobSeeker.Shared.PushNotifications.Interfaces;
using JobSeeker.Shared.PushNotifications.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Persistence
{
    public static class IdentityPersistanceServiceRegistration
    {
        public static IServiceCollection AddIdentityPersistanceServiceRegistration(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped(typeof(IWriteRepository<>), typeof(GenericWriteRepository<>));
            services.AddScoped<IIdentityUnitOfWOrk, IdentityUnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPushSubscriptionRepository, PushSubscriptionRepository>();
            services.AddScoped<IPushNotificationService, PushNotificationService>();
            services.AddScoped<IOutboxMessage, OutBoxRepository>();

            return services;
        }
    }
}
