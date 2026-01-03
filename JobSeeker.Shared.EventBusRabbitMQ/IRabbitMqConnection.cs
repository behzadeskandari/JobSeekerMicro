using RabbitMQ.Client;

namespace JobSeeker.Shared.EventBusRabbitMQ
{
    public interface IRabbitMqConnection : IDisposable
    {
        bool IsConnected { get; }
        // Changed to Task<bool> to match the implementation
        Task<bool> TryConnect();
        // Changed IModel to IChannel
        Task<IChannel> CreateChannelAsync();
    }
}
