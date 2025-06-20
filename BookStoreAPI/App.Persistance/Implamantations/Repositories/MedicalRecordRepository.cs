using App.Application.Interfaces.MedicalRecord;
using App.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace App.Persistance.Implamantations.Repositories
{
    public class MedicalRecordRepository(AppDbContext context) : GenericRepository<MedicalRecord>(context), IMedicalRecordRepository

    {
        public async Task<MedicalRecord?> GetMedicalRecordByDiagnosisAsync(string diagnosis)
        {
            return await context.MedicalRecords.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Diagnosis == diagnosis);
        }

        public async Task<MedicalRecord?> GetMedicalRecordByDoctorIdAsync(int doctorId)
        {
            return await context.MedicalRecords.AsNoTracking()
                 .SingleOrDefaultAsync(x=>x.DoctorId == doctorId);
        }

        public async Task<MedicalRecord?> GetMedicalRecordByPatientIdAsync(int patientId)
        {
            return await context.MedicalRecords.AsNoTracking()
                .SingleOrDefaultAsync(x=>x.PatientId == patientId);
        }

        public async Task<MedicalRecord?> GetMedicalRecordByRecordDateAsync(DateTime recordDate)
        {
            return await context.MedicalRecords
         .AsNoTracking()
         .SingleOrDefaultAsync(x => x.RecordDate.Date == recordDate.Date);
        }
    }
}
