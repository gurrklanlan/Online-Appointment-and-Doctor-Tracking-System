using App.Domain.Events.AddedEvent;
using MassTransit;

namespace App.Bus.Consumers
{
    public class DoctorDetailAddedEventConsumer : IConsumer<DoctorDetailAddedEvent>
    {
        public Task Consume(ConsumeContext<DoctorDetailAddedEvent> context)
        {
            Console.WriteLine($"Doctor Detail Added Event Received" +
                $"Specialty= {context.Message.Specialty}, Clinic Address= {context.Message.ClinicAddress}");

            return Task.CompletedTask;
        }
    }
}
