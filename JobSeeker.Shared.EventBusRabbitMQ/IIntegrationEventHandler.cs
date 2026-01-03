using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.EventBusRabbitMQ
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> where TIntegrationEvent : IntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent @event);
    }
}
