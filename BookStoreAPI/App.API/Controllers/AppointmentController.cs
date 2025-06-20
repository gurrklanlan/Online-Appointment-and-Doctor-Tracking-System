using App.Application.Features.Appointment.Create;
using App.Application.Features.Appointment.Update;
using App.Application.Interfaces.Appointment.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
  
    public class AppointmentController : CustomBaseController
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointmentAsync()
            =>CreateActionResult(await _appointmentService.GetAllAppointmentsAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAppointmentByIdAsync(int id)
            =>CreateActionResult(await _appointmentService.GetAppointmentByIdAsync(id));

        [HttpGet("patient/{patientId:int}")]
        public async Task<IActionResult> GetAppointmentsByPatientIdAsync(int patientId)
      => CreateActionResult(await _appointmentService.GetAppointmentsByPatientIdAsync(patientId));

        [HttpGet("doctor/{doctorId:int}")]
        public async Task<IActionResult> GetAppointmentsByDoctorIdAsync(int doctorId)
            => CreateActionResult(await _appointmentService.GetAppointmentsByDoctorIdAsync(doctorId));

        [HttpGet("by-date/{date}")]
        public async Task<IActionResult> GetAppointmentsByDateAsync(string date)
        {
            if (!DateTime.TryParse(date, out var parsedDate))
                return BadRequest("Geçersiz tarih formatı. Örnek: 2025-06-20");

            var result = await _appointmentService.GetAppointmentsByDateAsync(parsedDate);
            return CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointmentasync(CreateAppointmentRequest request)
            => CreateActionResult(await _appointmentService.CreateAppointmentAsync(request));

        [HttpPut]
        public async Task<IActionResult> UpdateAppointmentAsync(UpdateAppointmentRequest request)
            =>CreateActionResult(await _appointmentService.UpdateAppointmentAsync(request));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAppointmentAsync(int id)
            =>CreateActionResult(await _appointmentService.DeleteAppointmentAsync(id));
    }
}
