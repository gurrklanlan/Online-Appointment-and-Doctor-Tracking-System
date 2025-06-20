using App.Application.Features.MedicalRecord;
using App.Application.Features.MedicalRecord.Create;
using App.Application.Features.MedicalRecord.Update;

namespace App.Application.Interfaces.MedicalRecord.Service
{
    public interface IMedicalService
    {
        Task<ServiceResult<List<MedicalRecordDto>>> GetAllMedicalRecordsAsync();
        Task<ServiceResult<MedicalRecordDto>> GetMedicalRecordByIdAsync(int medicalRecordId);
        Task<ServiceResult<CreateMedicalRecordRequest>> CreateMedicalRecordAsync(CreateMedicalRecordRequest medicalRecord);
        Task<ServiceResult<UpdateMedicalRecordRequest>> UpdateMedicalRecordAsync(UpdateMedicalRecordRequest medicalRecord);
        Task<ServiceResult<MedicalRecordDto>> DeleteMedicalRecordAsync(int medicalRecordId);   
        Task<ServiceResult<List<MedicalRecordDto>>> GetMedicalRecordByDiagnosisAsync(string diagnosis);
       
    }
}
