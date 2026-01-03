using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MassTransitTestApp
{
    // Test event matching the ProfileService event structure
    public class TestUserRegisteredEvent
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = "test@example.com";
        public string Role { get; set; } = "User";
        public string? FirstName { get; set; } = "Test";
        public string? LastName { get; set; } = "User";
    }

    // Test consumer
    public class TestUserRegisteredConsumer : IConsumer<TestUserRegisteredEvent>
    {
        private readonly ILogger<TestUserRegisteredConsumer> _logger;

        public TestUserRegisteredConsumer(ILogger<TestUserRegisteredConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<TestUserRegisteredEvent> context)
        {
            _logger.LogInformation("Test Consumer: Received UserRegisteredEvent for UserId {UserId}", context.Message.UserId);
            await Task.Delay(100); // Simulate processing time
            _logger.LogInformation("Test Consumer: Processing completed for UserId {UserId}", context.Message.UserId);
        }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("MassTransit Connection Optimization Test App");
            Console.WriteLine("==========================================");

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddMassTransit(x =>
                    {
                        // Register test consumer with optimized settings
                        x.AddConsumer<TestUserRegisteredConsumer>()
                            .Endpoint(e => e.ConcurrentMessageLimit = 1);

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("rabbitmq://localhost", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            // Optimized settings matching ProfileService
                            cfg.PrefetchCount = 4;

                            cfg.ReceiveEndpoint("test-user-registered-event", e =>
                            {
                                e.ConfigureConsumer<TestUserRegisteredConsumer>(context);
                                e.PrefetchCount = 1;
                            });

                            cfg.ConfigureEndpoints(context);
                        });
                    });
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .Build();

            await host.StartAsync();

            var bus = host.Services.GetRequiredService<IBus>();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            Console.WriteLine("Test App started. Publishing test messages...");
            Console.WriteLine("Check RabbitMQ Management UI for connection efficiency");
            Console.WriteLine("Press Ctrl+C to stop");

            // Publish test messages to simulate load
            for (int i = 0; i < 10; i++)
            {
                await bus.Publish(new TestUserRegisteredEvent
                {
                    UserId = Guid.NewGuid().ToString(),
                    Email = $"test{i}@example.com",
                    FirstName = $"Test{i}",
                    LastName = "User"
                });

                logger.LogInformation("Published test message {Count}/10", i + 1);
                await Task.Delay(500); // Delay between messages
            }

            Console.WriteLine("All test messages published. Monitoring consumption...");
            Console.WriteLine("Press Ctrl+C to exit");

            // Keep the app running to observe connections
            await host.WaitForShutdownAsync();
        }
    }
}

