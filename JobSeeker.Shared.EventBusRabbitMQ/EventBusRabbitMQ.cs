using JobSeeker.Shared.Contracts.Integration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace JobSeeker.Shared.EventBusRabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        private readonly IRabbitMqConnection _connection;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _queueName;
        private readonly string _exchangeName;

        // v7: IModel is now IChannel
        private IChannel? _consumerChannel;
        private readonly ConcurrentDictionary<string, Type> _eventTypes = new();
        private readonly ConcurrentDictionary<string, List<Type>> _eventHandlers = new();

        public EventBusRabbitMQ(
            IRabbitMqConnection connection,
            ILogger<EventBusRabbitMQ> logger,
            IServiceProvider serviceProvider,
            string queueName = "jobseeker-events",
            string exchangeName = "jobseeker-exchange")
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _queueName = queueName;
            _exchangeName = exchangeName;
        }

        public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
        {
            // v7: CreateChannelAsync replaces CreateModel
            using var channel = await _connection.CreateChannelAsync();

            var routingKey = @event.GetType().Name;

            // v7: All Declare/Bind methods are now Async
            await channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Direct, durable: true);
            await channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false);
            await channel.QueueBindAsync(_queueName, _exchangeName, routingKey);

            var message = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(message);

            // v7: Properties are handled via BasicProperties class
            var properties = new BasicProperties
            {
                Persistent = true,
                Type = routingKey
            };

            // v7: BasicPublishAsync
            await channel.BasicPublishAsync(_exchangeName, routingKey, mandatory: false, basicProperties: properties, body: body);

            _logger.LogInformation("Published event {EventType} with ID {EventId}", routingKey, @event.Id);
        }

        public Task SubscribeAsync<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;
            if (!_eventTypes.ContainsKey(eventName)) _eventTypes[eventName] = typeof(T);

            var handlers = _eventHandlers.GetOrAdd(eventName, _ => new List<Type>());
            if (!handlers.Contains(typeof(TH))) handlers.Add(typeof(TH));

            return Task.CompletedTask;
        }

        public async Task StartConsuming()
        {
            if (_consumerChannel == null || !_consumerChannel.IsOpen)
            {
                _consumerChannel = await CreateConsumerChannelAsync();
            }

            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var eventName = ea.RoutingKey ?? ea.BasicProperties.Type;
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                try
                {
                    await ProcessEvent(eventName!, message);
                    // v7: Acks are Async
                    await _consumerChannel.BasicAckAsync(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing event {EventName}", eventName);
                    await _consumerChannel.BasicNackAsync(ea.DeliveryTag, false, true);
                }
            };

            await _consumerChannel.BasicConsumeAsync(_queueName, autoAck: false, consumer: consumer);
        }

        public async Task ProcessEvent(string eventName, string message)
        {
            if (!_eventTypes.ContainsKey(eventName)) return;

            var eventType = _eventTypes[eventName];
            var eventInstance = JsonSerializer.Deserialize(message, eventType) as IntegrationEvent;

            if (eventInstance == null) return;

            using var scope = _serviceProvider.CreateScope();
            if (_eventHandlers.TryGetValue(eventName, out var handlerTypes))
            {
                foreach (var handlerType in handlerTypes)
                {
                    var handler = scope.ServiceProvider.GetService(handlerType);
                    if (handler == null) continue;

                    var method = handlerType.GetMethod("HandleAsync");
                    if (method != null)
                    {
                        await (Task)method.Invoke(handler, new object[] { eventInstance })!;
                    }
                }
            }
        }

        public async Task<IChannel> CreateConsumerChannelAsync()
        {
            var channel = await _connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Direct, durable: true);
            await channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false);

            return channel;
        }

        public void Dispose()
        {
            _consumerChannel?.Dispose();
        }
    }
}
