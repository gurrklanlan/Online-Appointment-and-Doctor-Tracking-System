namespace App.Application.Features.Appointment.Create
{
    public class CreateAppointmentRequest
    {
        public DateTime AppointmentDate { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

    }
}
