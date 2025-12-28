using System;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdvertisementService.Infrastructure
{
    public static class AdvertismentInfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureAdvertismentInfrastructureServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            // Register HttpClientFactory
            services.AddHttpClient();

            // Get service URLs from configuration
            var identityServiceBaseUrl = configuration["ServiceUrls:IdentityService"] ?? "https://localhost:5001";
            var jobServiceBaseUrl = configuration["ServiceUrls:JobService"] ?? "https://localhost:5002";

            // Register the client implementation
            services.AddScoped<IIdentityServiceClient>(sp =>
            {
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var logger = sp.GetRequiredService<ILogger<IdentityServiceClient>>();
                
                return new IdentityServiceClient(
                    httpClientFactory,
                    identityServiceBaseUrl,
                    jobServiceBaseUrl,
                    logger);
            });

            return services;
        }
    }
}
