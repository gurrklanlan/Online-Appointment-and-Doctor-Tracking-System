namespace App.Domain.Events.AddedEvent;

    public record MedicalRecordAddedEvent(string Diagnosis, string Prescription, DateTime RecordDate):IMessageOrEvent;
