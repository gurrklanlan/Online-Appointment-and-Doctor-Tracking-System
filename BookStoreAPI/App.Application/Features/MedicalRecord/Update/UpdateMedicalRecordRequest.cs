namespace App.Application.Features.MedicalRecord.Update
{
    public class UpdateMedicalRecordRequest
    {
        public int Id { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public int? AppointmentId { get; set; }
    }
}
