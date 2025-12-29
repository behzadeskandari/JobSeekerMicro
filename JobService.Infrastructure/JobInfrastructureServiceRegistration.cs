using JobService.Application.Interfaces;
using JobService.Infrastructure.DomainEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobService.Infrastructure
{
    public static class JobInfrastructureServiceRegistration
    {
        public static IServiceCollection AddJobInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            // Register domain event dispatcher
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            return services;
        }
    }
}
