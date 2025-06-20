using App.Application.Features.Appointment;
using App.Application.Features.Appointment.Create;
using App.Application.Features.Appointment.Update;

namespace App.Application.Interfaces.Appointment.Service
{
    public interface IAppointmentService
    {
        Task<ServiceResult<List<AppointmentDto>>> GetAllAppointmentsAsync();
        Task<ServiceResult<CreateAppointmentRequest>> CreateAppointmentAsync(CreateAppointmentRequest appointment);
        Task<ServiceResult<UpdateAppointmentRequest>> UpdateAppointmentAsync(UpdateAppointmentRequest appointment);
        Task<ServiceResult<AppointmentDto>> DeleteAppointmentAsync(int appointmentId);
        Task<ServiceResult<AppointmentDto?>> GetAppointmentByIdAsync(int appointmentId);
        Task<ServiceResult<List<AppointmentDto>>> GetAppointmentsByPatientIdAsync(int patientId);
        Task<ServiceResult<List<AppointmentDto>>>  GetAppointmentsByDoctorIdAsync(int doctorId);
        Task<ServiceResult<List<AppointmentDto>>> GetAppointmentsByDateAsync(DateTime date);
    }
}
