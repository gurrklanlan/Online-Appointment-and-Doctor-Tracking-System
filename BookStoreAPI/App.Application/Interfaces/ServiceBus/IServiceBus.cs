using App.Domain.Events;

namespace App.Application.Interfaces.ServiceBus
{
    public interface IServiceBus
    {
        Task<T> PublisAsync<T>(T @event, CancellationToken cancellationToken = default)
            where T : IMessageOrEvent;

        Task<T> SendAsync<T>(T message, string queueName, CancellationToken cancellationToken = default)
            where T : IMessageOrEvent;
    }
}
