using App.Application.Interfaces.Appointment;
using App.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace App.Persistance.Implamantations.Repositories
{
    public class AppointmentRepository(AppDbContext context) : GenericRepository<Appointment>(context), IAppointmentRepository
    {
        public async Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            return context.Appointments.AsNoTracking()
                .Where(a => a.AppointmentDate.Date == date.Date)
                .ToList();
        }

        public Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            return context.Appointments
                .AsNoTracking()
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

        public Task<List<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            return context.Appointments.AsNoTracking()
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
        }
    }
}
