using RabbitMQ.Client;

namespace JobSeeker.Shared.EventBusRabbitMQ
{
    public interface IRabbitMqConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
