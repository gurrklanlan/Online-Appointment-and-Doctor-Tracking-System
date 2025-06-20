namespace App.Domain.Events.AddedEvent;

    public record DoctorDetailAddedEvent(string Specialty, string ClinicAddress):IMessageOrEvent;
    
