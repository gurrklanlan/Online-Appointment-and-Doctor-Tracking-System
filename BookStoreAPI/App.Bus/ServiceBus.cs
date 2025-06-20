using App.Application.Interfaces.ServiceBus;
using App.Domain.Events;
using MassTransit;

namespace App.Bus
{
    public class ServiceBus(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider) : IServiceBus
    {
        public Task<T> PublisAsync<T>(T @event, CancellationToken cancellationToken = default) where T : IMessageOrEvent
        {
            publishEndpoint.Publish(@event, cancellationToken);
            return Task.FromResult(@event);
        }

        public async Task<T> SendAsync<T>(T message, string queueName, CancellationToken cancellationToken = default) where T : IMessageOrEvent
        {
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));

            await endpoint.Send(message, cancellationToken);

            return message;
        }
    }
}
