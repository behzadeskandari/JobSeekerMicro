using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.EventBusRabbitMQ
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : IntegrationEvent;
        Task SubscribeAsync<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        void StartConsuming();
        void Dispose();
    }
}
