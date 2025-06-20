namespace App.Application.Interfaces.MedicalRecord
{
    public interface IMedicalRecordRepository:IGenericRepository<App.Domain.Entites.MedicalRecord>
    {
        Task<App.Domain.Entites.MedicalRecord?> GetMedicalRecordByPatientIdAsync(int patientId);
        Task<App.Domain.Entites.MedicalRecord?> GetMedicalRecordByDoctorIdAsync(int doctorId);
        Task<App.Domain.Entites.MedicalRecord?> GetMedicalRecordByRecordDateAsync(DateTime recordDate);
        Task<App.Domain.Entites.MedicalRecord?> GetMedicalRecordByDiagnosisAsync(string diagnosis);
    }

}
