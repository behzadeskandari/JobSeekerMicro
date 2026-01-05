using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace JobSeeker.Shared.EventBusRabbitMQ
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBusRabbitMQ(
            this IServiceCollection services,
            string connectionString = "amqp://guest:guest@localhost:5672/",
            string queueName = "jobseeker-events",
            string exchangeName = "jobseeker-exchange")
        {
            // Parse connection string
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(connectionString),
                //DispatchConsumersAsync = true
            };

            // Register RabbitMQ connection
            services.AddSingleton<IRabbitMqConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RabbitMqConnection>>();
                return new RabbitMqConnection(connectionFactory);
            });

            // Register event bus
            services.AddSingleton<IEventBus>(sp =>
            {
                var connection = sp.GetRequiredService<IRabbitMqConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var serviceProvider = sp.GetRequiredService<IServiceProvider>();
                return new EventBusRabbitMQ(connection, logger, serviceProvider, queueName, exchangeName);
            });

            return services;
        }
    }
}
