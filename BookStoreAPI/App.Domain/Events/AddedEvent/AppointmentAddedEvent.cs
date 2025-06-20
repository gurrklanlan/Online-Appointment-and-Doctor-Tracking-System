namespace App.Domain.Events.AddedEvent;

    public record AppointmentAddedEvent(DateTime AppointmentDate, string Status):IMessageOrEvent;
 