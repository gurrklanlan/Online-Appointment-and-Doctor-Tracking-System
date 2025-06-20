using App.Domain.Events.AddedEvent;
using MassTransit;

namespace App.Bus.Consumers
{
    public class MedicalRecordAddedEventConsumer : IConsumer<MedicalRecordAddedEvent>
    {
        public Task Consume(ConsumeContext<MedicalRecordAddedEvent> context)
        {
            Console.WriteLine($"Medical Record Added Event Received: Diagnosis= " +
                $"{context.Message.Diagnosis}, Prescription= {context.Message.Prescription}, Record Date= {context.Message.RecordDate}");

            return Task.CompletedTask;
        }
    }
}
