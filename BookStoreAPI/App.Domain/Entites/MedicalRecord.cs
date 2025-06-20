namespace App.Domain.Entites
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime RecordDate { get; set; }
        public string Diagnosis { get; set; } = null!;
        public string Prescription { get; set; } = null!;

        // Navigation
        public User Patient { get; set; } = null!;
        public User Doctor { get; set; } = null!;
    }

}
