using System.Net;
using System.Net.Http.Headers;
using App.Application.Features.MedicalRecord;
using App.Application.Features.MedicalRecord.Create;
using App.Application.Features.MedicalRecord.Update;
using App.Application.Interfaces.Caching;
using App.Domain.Entites;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace App.Application.Interfaces.MedicalRecord.Service
{
    public class MedicalService : IMedicalService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<App.Domain.Entites.MedicalRecord> _logger;
        private readonly ICacheService _cacheService;
        public MedicalService(IMedicalRecordRepository medicalRecordRepository, IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<App.Domain.Entites.MedicalRecord> logger, ICacheService cacheService)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _cacheService = cacheService;
        }

        private const string MedicalRecordCachedKey = "medicalRecodCachedKey";
        public async Task<ServiceResult<CreateMedicalRecordRequest>> CreateMedicalRecordAsync(CreateMedicalRecordRequest medicalRecord)
        {
            try
            {
                var anyMedicalRecord = _medicalRecordRepository.GetAll()
               .Any(m => m.Id == medicalRecord.AppointmentId);
                if (anyMedicalRecord)
                    return ServiceResult<CreateMedicalRecordRequest>.Fail("Medical record with the same patient, doctor and date already exists.");
                var medicalRecordAsDto = _mapper.Map<App.Domain.Entites.MedicalRecord>(medicalRecord);
                await _medicalRecordRepository.AddAsync(medicalRecordAsDto);
                await _unitOfWork.SaveChangesAsync();
                return ServiceResult<CreateMedicalRecordRequest>.Success(medicalRecord);

            }
            catch (Exception)
            {

               _logger.LogError("There is an error while creating medical record");
                return ServiceResult<CreateMedicalRecordRequest>.Fail("Hata");
            }
          
        }

        public async Task<ServiceResult<MedicalRecordDto>> DeleteMedicalRecordAsync(int medicalRecordId)
        {
            var medicalRecord = await _medicalRecordRepository.GetByIdAsync(medicalRecordId);
            if (medicalRecord == null)
                return ServiceResult<MedicalRecordDto>.Fail("Medical record not found", HttpStatusCode.NotFound);
            await Task.Run(() => _medicalRecordRepository.DeleteAsync(medicalRecord));
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<MedicalRecordDto>.Success(_mapper.Map<MedicalRecordDto>(medicalRecord));
        }

        public async Task<ServiceResult<List<MedicalRecordDto>>> GetAllMedicalRecordsAsync()
        {
            var medAsCached = await _cacheService.GetAsync<List<MedicalRecordDto>>(MedicalRecordCachedKey);
            if (medAsCached is not null)
                _logger.LogError("Is not exist", MedicalRecordCachedKey);


            var medicalRecord = _medicalRecordRepository.GetAll().ToList();
            if (medicalRecord == null)
                return ServiceResult<List<MedicalRecordDto>>.Fail("No medical records found", HttpStatusCode.NotFound);
            var medicalRecordDtos = _mapper.Map<List<MedicalRecordDto>>(medicalRecord);

            await _cacheService.AddAsync(MedicalRecordCachedKey, medicalRecordDtos, TimeSpan.FromSeconds(15));
            return ServiceResult<List<MedicalRecordDto>>.Success(medicalRecordDtos);
        }

        public async Task<ServiceResult<List<MedicalRecordDto>>> GetMedicalRecordByDiagnosisAsync(string diagnosis)
        {
            var medicalRecord = _medicalRecordRepository.GetMedicalRecordByDiagnosisAsync(diagnosis);
            if (medicalRecord == null)
                return ServiceResult<List<MedicalRecordDto>>.Fail("No medical records found for the given diagnosis", HttpStatusCode.NotFound);
            var medicalRecordDtos = _mapper.Map<List<MedicalRecordDto>>(medicalRecord);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<List<MedicalRecordDto>>.Success(medicalRecordDtos);
        }

        public async Task<ServiceResult<MedicalRecordDto>> GetMedicalRecordByIdAsync(int medicalRecordId)
        {
            var medicalRecord = await _medicalRecordRepository.GetByIdAsync(medicalRecordId);
            if (medicalRecord == null)
                return ServiceResult<MedicalRecordDto>.Fail("Medical record not found", HttpStatusCode.NotFound);
            var medicalRecordDto = _mapper.Map<MedicalRecordDto>(medicalRecord);
            return ServiceResult<MedicalRecordDto>.Success(medicalRecordDto);

        }
     
        public async Task<ServiceResult<UpdateMedicalRecordRequest>> UpdateMedicalRecordAsync(UpdateMedicalRecordRequest medicalRecord)
        {
           var medAsDto = _mapper.Map<App.Domain.Entites.MedicalRecord>(medicalRecord);
            var existingRecord = await _medicalRecordRepository.GetByIdAsync(medAsDto.Id);
            if (existingRecord == null)
                return ServiceResult<UpdateMedicalRecordRequest>.Fail("Medical record not found", HttpStatusCode.NotFound);
            existingRecord.Diagnosis = medAsDto.Diagnosis;
            existingRecord.RecordDate = medAsDto.RecordDate.Date;
            existingRecord.Patient = medAsDto.Patient;
            existingRecord.Prescription = medAsDto.Prescription;
            existingRecord.Doctor = medAsDto.Doctor;
            existingRecord.DoctorId = medAsDto.DoctorId;
            existingRecord.PatientId = medAsDto.PatientId;
            existingRecord.Id = medAsDto.Id;
            
            _medicalRecordRepository.UpdateAsync(existingRecord);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<UpdateMedicalRecordRequest>.Success(medicalRecord);
        }
    }
}
