using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.EventBusRabbitMQ
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection? _connection;
        private bool _disposed;

        public RabbitMqConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            // Note: In v7, you might prefer calling TryConnect externally 
            // since constructor-based async calls are tricky.
        }

        public bool IsConnected => _connection is { IsOpen: true } && !_disposed;

        public async Task<bool> TryConnect()
        {
            try
            {
                _connection = await _connectionFactory.CreateConnectionAsync();
                return IsConnected;
            }
            catch (BrokerUnreachableException)
            {
                await Task.Delay(2000); // Use Task.Delay instead of Thread.Sleep in async
                _connection = await _connectionFactory.CreateConnectionAsync();
                return IsConnected;
            }
        }

        public async Task<IChannel> CreateChannelAsync()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No Rabbit Connection");
            }
            // CreateModel() is now CreateChannelAsync()
            return await _connection!.CreateChannelAsync();
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _connection?.Dispose();
        }
    }
}
