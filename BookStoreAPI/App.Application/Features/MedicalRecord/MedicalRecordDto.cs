namespace App.Application.Features.MedicalRecord
{
    public class MedicalRecordDto
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; } = null!;
        public string Treatment { get; set; } = null!;
        public int AppointmentId { get; set; }
    }
}
