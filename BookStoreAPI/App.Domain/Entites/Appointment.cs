namespace App.Domain.Entites
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = "Pending"; // "Pending", "Approved", "Cancelled"

        // Navigation
        public User Patient { get; set; } = null!;
        public User Doctor { get; set; } = null!;
    }

}