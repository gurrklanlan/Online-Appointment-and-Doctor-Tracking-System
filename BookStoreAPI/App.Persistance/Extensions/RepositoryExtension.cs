using App.Application.Interfaces;
using App.Application.Interfaces.Appointment;
using App.Application.Interfaces.DoctorDetail;
using App.Application.Interfaces.MedicalRecord;
using App.Application.Interfaces.User;
using App.Domain.Options;
using App.Persistance.Implamantations;
using App.Persistance.Implamantations.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Persistance.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepoEx(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = configuration.GetSection(ConnectionStringOption.Key)
                .Get<ConnectionStringOption>();

                options.UseSqlServer(connectionString!.SqlServer, sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.MigrationsAssembly
                    (typeof(PersistanceAssembly).Assembly.FullName);

                });

            });





            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
            services.AddScoped<IDoctorDetailRepository, DoctorDetailRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
