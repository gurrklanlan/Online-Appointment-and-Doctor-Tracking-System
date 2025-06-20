namespace App.Application.Interfaces.Appointment
{
    public interface IAppointmentRepository:IGenericRepository<App.Domain.Entites.Appointment>
    {
       
        Task<List<App.Domain.Entites.Appointment>> GetAppointmentsByPatientIdAsync(int patientId);
        Task<List<App.Domain.Entites.Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);
        Task<List<App.Domain.Entites.Appointment>> GetAppointmentsByDateAsync(DateTime date);
        
    }
}
