namespace App.Application.Features.Appointment.Update
{
    public class UpdateAppointmentRequest
    {
        public int Id { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public string? Status { get; set; }
    }
}
