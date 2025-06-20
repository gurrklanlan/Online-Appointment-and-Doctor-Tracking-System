using System.Reflection;
using App.Application.Interfaces.Appointment.Service;
using App.Application.Interfaces.DoctorDetail.Service;
using App.Application.Interfaces.MedicalRecord.Service;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using App.Application.Interfaces.User.Service;

namespace App.Application.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServicesEx (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            services.AddScoped<IMedicalService, MedicalService>();
            services.AddScoped<IDoctorDetailService, DoctorDetailService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
