using System.Net;
using App.Application.Features.Appointment;
using App.Application.Features.Appointment.Create;
using App.Application.Features.Appointment.Update;
using App.Application.Interfaces.Caching;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace App.Application.Interfaces.Appointment.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly ILogger<App.Domain.Entites.Appointment> _logger;
        public AppointmentService(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<App.Domain.Entites.Appointment> logger, ICacheService cacheService)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
            _logger = logger;
        }

        private const string AppointmentCacheKey = "appointmentCacheKey";
        public async Task<ServiceResult<CreateAppointmentRequest>> CreateAppointmentAsync(CreateAppointmentRequest appointment)
        {
            try
            {
                var anyAppointment = _appointmentRepository.GetAll()
                .Any(a => a.PatientId == appointment.PatientId && a.DoctorId == appointment.DoctorId);
                if (anyAppointment)
                    return ServiceResult<CreateAppointmentRequest>.Fail("Appointment with the same patient, doctor already exists.");
                var appointmentAsDto = _mapper.Map<App.Domain.Entites.Appointment>(appointment);
                await _appointmentRepository.AddAsync(appointmentAsDto);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("You've be success");
                return ServiceResult<CreateAppointmentRequest>.Success(appointment);

            }
            catch (Exception)
            {

                _logger.LogError("HATAAAAA!!!!!!!!!!!!!!!");
                return ServiceResult<CreateAppointmentRequest>.Fail("Hata");
            }
          
        }

        public async Task<ServiceResult<AppointmentDto>> DeleteAppointmentAsync(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
                return ServiceResult<AppointmentDto>.Fail("Appointment not found", HttpStatusCode.NotFound);
            await Task.Run(() => _appointmentRepository.DeleteAsync(appointment));
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<AppointmentDto>.Success(_mapper.Map<AppointmentDto>(appointment));
        }

        public async Task<ServiceResult<List<AppointmentDto>>> GetAllAppointmentsAsync()
        {
            var appointmentAsCached = await _cacheService.GetAsync<List<AppointmentDto>>(AppointmentCacheKey);
            if(appointmentAsCached is not null)
                _logger.LogInformation(AppointmentCacheKey);

            var appointments =  _appointmentRepository.GetAll().ToList(); // Await the task to get the result
            if (appointments == null || !appointments.Any())
                return ServiceResult<List<AppointmentDto>>.Fail("No appointments found", HttpStatusCode.NotFound);
            var appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);

            await _cacheService.AddAsync(AppointmentCacheKey, appointmentDtos, TimeSpan.FromMinutes(15));
            return ServiceResult<List<AppointmentDto>>.Success(appointmentDtos);
        }

        public async Task<ServiceResult<AppointmentDto?>> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
                return ServiceResult<AppointmentDto?>.Fail("Appointment not found", HttpStatusCode.NotFound);
            var appointmentDto = _mapper.Map<AppointmentDto>(await appointment);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<AppointmentDto?>.Success(appointmentDto);


        }

        public async Task<ServiceResult<List<AppointmentDto>>> GetAppointmentsByDateAsync(DateTime date)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByDateAsync(date); // Await the task to get the result
            if (appointments == null || !appointments.Any())
                return ServiceResult<List<AppointmentDto>>.Fail("No appointments found for the given date", HttpStatusCode.NotFound);
            var appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);
            return ServiceResult<List<AppointmentDto>>.Success(appointmentDtos);
        }

        public async Task<ServiceResult<List<AppointmentDto>>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByDoctorIdAsync(doctorId); 
            if (appointments == null || !appointments.Any())
                return ServiceResult<List<AppointmentDto>>.Fail("No appointments found for the given doctor", HttpStatusCode.NotFound);
            var appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);
            return ServiceResult<List<AppointmentDto>>.Success(appointmentDtos);
        }

        public async Task<ServiceResult<List<AppointmentDto>>> GetAppointmentsByPatientIdAsync(int patientId)
        {

            var appointments = await _appointmentRepository.GetAppointmentsByPatientIdAsync(patientId); 
            if (appointments == null || !appointments.Any())
                return ServiceResult<List<AppointmentDto>>.Fail("No appointments found for the given patient", HttpStatusCode.NotFound);
            var appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);
            return ServiceResult<List<AppointmentDto>>.Success(appointmentDtos);

        }

        public async Task<ServiceResult<UpdateAppointmentRequest>> UpdateAppointmentAsync(UpdateAppointmentRequest appointment)
        {
          var appointmentAsDto = _mapper.Map<App.Domain.Entites.Appointment>(appointment);
            var existingAppointment = await _appointmentRepository.GetByIdAsync(appointmentAsDto.Id);
            if (existingAppointment == null)
                return ServiceResult<UpdateAppointmentRequest>.Fail("Appointment not found", HttpStatusCode.NotFound);
            existingAppointment.AppointmentDate = appointmentAsDto.AppointmentDate;
            existingAppointment.PatientId = appointmentAsDto.PatientId;
            existingAppointment.Patient = appointmentAsDto.Patient;
            existingAppointment.DoctorId = appointmentAsDto.DoctorId;
            existingAppointment.Doctor = appointmentAsDto.Doctor;
            existingAppointment.Status = appointmentAsDto.Status;
          
            _appointmentRepository.UpdateAsync(appointmentAsDto);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<UpdateAppointmentRequest>.Success(appointment);
        }
    }
}
