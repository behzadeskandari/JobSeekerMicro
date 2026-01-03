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
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly IRabbitMqConnection _connection;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _queueName;
        private readonly string _exchangeName;
        private IModel _consumerChannel;
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

            _consumerChannel = CreateConsumerChannel();
        }

        public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
        {
            if (!@event.GetType().IsAssignableTo(typeof(IntegrationEvent)))
            {
                throw new ArgumentException("Event must inherit from IntegrationEvent");
            }

            var channel = _connection.CreateModel();

            try
            {
                // Declare exchange
                channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct, durable: true);

                // Declare queue
                channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);

                // Bind queue to exchange with routing key (use event type name)
                var routingKey = @event.GetType().Name;
                channel.QueueBind(_queueName, _exchangeName, routingKey);

                var message = JsonSerializer.Serialize(@event);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.Type = routingKey;

                channel.BasicPublish(_exchangeName, routingKey, properties, body);

                _logger.LogInformation("Published event {EventType} with ID {EventId}", routingKey, @event.Id);
            }
            finally
            {
                channel.Dispose();
            }
        }

        public Task SubscribeAsync<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var eventType = typeof(T);
            var eventName = eventType.Name;
            var handlerType = typeof(TH);

            if (!_eventTypes.ContainsKey(eventName))
            {
                _eventTypes[eventName] = eventType;
            }

            if (!_eventHandlers.ContainsKey(eventName))
            {
                _eventHandlers[eventName] = new List<Type>();
            }

            if (!_eventHandlers[eventName].Contains(handlerType))
            {
                _eventHandlers[eventName].Add(handlerType);
            }

            _logger.LogInformation("Subscribed handler {HandlerType} for event {EventType}", handlerType.Name, eventName);

            return Task.CompletedTask;
        }

        public void StartConsuming()
        {
            if (_consumerChannel == null || _consumerChannel.IsClosed)
            {
                _consumerChannel = CreateConsumerChannel();
            }

            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                try
                {
                    await ProcessEvent(eventName, message);
                    _consumerChannel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing event {EventName}", eventName);
                    // In a production system, you might want to implement dead letter queues
                    // For now, we'll nack the message and let RabbitMQ handle redelivery
                    _consumerChannel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            _consumerChannel.BasicConsume(_queueName, false, consumer);

            _logger.LogInformation("Started consuming events from queue {QueueName}", _queueName);
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (!_eventTypes.ContainsKey(eventName) || !_eventHandlers.ContainsKey(eventName))
            {
                _logger.LogWarning("No handlers registered for event {EventName}", eventName);
                return;
            }

            var eventType = _eventTypes[eventName];
            var eventInstance = JsonSerializer.Deserialize(message, eventType) as IntegrationEvent;

            if (eventInstance == null)
            {
                _logger.LogError("Failed to deserialize event {EventName}", eventName);
                return;
            }

            var handlerTypes = _eventHandlers[eventName];

            using var scope = _serviceProvider.CreateScope();

            foreach (var handlerType in handlerTypes)
            {
                var handler = scope.ServiceProvider.GetService(handlerType);
                if (handler == null)
                {
                    _logger.LogError("Handler {HandlerType} not found in service provider", handlerType.Name);
                    continue;
                }

                var method = handlerType.GetMethod("HandleAsync");
                if (method == null)
                {
                    _logger.LogError("HandleAsync method not found on handler {HandlerType}", handlerType.Name);
                    continue;
                }

                var task = (Task)method.Invoke(handler, new object[] { eventInstance });
                await task;

                _logger.LogInformation("Handled event {EventName} with handler {HandlerType}", eventName, handlerType.Name);
            }
        }

        private IModel CreateConsumerChannel()
        {
            var channel = _connection.CreateModel();

            // Declare exchange
            channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct, durable: true);

            // Declare queue
            channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);

            channel.CallbackException += (sender, ea) =>
            {
                _logger.LogError(ea.Exception, "Recreating RabbitMQ consumer channel");

                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartConsuming();
            };

            return channel;
        }

        public void Dispose()
        {
            _consumerChannel?.Dispose();
        }
    }
}
