using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Persistence.DomainEvents;
using AdvertisementService.Persistence.GenericRepository;
using AdvertisementService.Persistence.Repository;
using AdvertisementService.Persistence.UnitOfWork;
using JobSeeker.Shared.Common.Interfaces;
using JobSeeker.Shared.PushNotifications.Interfaces;
using JobSeeker.Shared.PushNotifications.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertisementService.Persistence
{
    public static class AdvertismentPersistanceServiceRegistration
    {
        public static IServiceCollection AddAdvertismentPersistanceServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IWriteRepository<>), typeof(GenericWriteRepository<>));
            
            // Register UnitOfWork with domain event dispatcher
            services.AddScoped<IAdvertisementUnitOfWork>(sp =>
            {
                var context = sp.GetRequiredService<AdvertisementService.Persistence.DbContexts.AdvertismentDbContext>();
                var dispatcher = sp.GetService<AdvertisementService.Application.Interfaces.IDomainEventDispatcher>();
                return new AdvertisementService.Persistence.UnitOfWork.AdvertisementUnitOfWork(context, dispatcher);
            });
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
            services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPricingCategoryRepository, PricingCategoryRepository>();
            services.AddScoped<IPricingFeaturesRepository, PricingFeaturesRepository>();
            services.AddScoped<IPricingPlanRepository, PricingPlanRepository>();
            services.AddScoped<IProductInventoryRepository, ProductInventoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISalesOrderItemRepository, SalesOrderItemRepository>();
            services.AddScoped<ISalesOrderRepository, SalesOrderRepository>();
            services.AddScoped<JobSeeker.Shared.PushNotifications.Interfaces.IPushSubscriptionRepository, AdvertisementService.Persistence.Repository.PushSubscriptionRepository>();
            services.AddScoped<IPushNotificationService, PushNotificationService>();
            return services;
        }
    }
}
