using JobSeeker.Shared.Contracts.Integration;
using RabbitMQ.Client;

namespace JobSeeker.Shared.EventBusRabbitMQ
{
    public interface IEventBus
    {
        public Task PublishAsync<T>(T @event) where T : IntegrationEvent;

        public Task SubscribeAsync<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        public Task StartConsuming();
        public Task ProcessEvent(string eventName, string message);
        public Task<IChannel> CreateConsumerChannelAsync();
        void Dispose();
    }
}
