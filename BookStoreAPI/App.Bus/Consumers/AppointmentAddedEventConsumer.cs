using App.Domain.Events.AddedEvent;
using MassTransit;

namespace App.Bus.Consumers
{
    public class AppointmentAddedEventConsumer : IConsumer<AppointmentAddedEvent>
    {
        public Task Consume(ConsumeContext<AppointmentAddedEvent> context)
        {
            Console.WriteLine($"Appointments Added Event Received: " +
                $"Appointment Date= {context.Message.AppointmentDate}, Status= {context.Message.Status}");
            return Task.CompletedTask;
        }
    }
}
