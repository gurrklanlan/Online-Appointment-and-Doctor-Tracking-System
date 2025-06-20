namespace App.Application.Features.MedicalRecord.Create
{
    public class CreateMedicalRecordRequest
    {
        public string Diagnosis { get; set; } = null!;
        public string Treatment { get; set; } = null!;
        public int AppointmentId { get; set; }
    }
}
