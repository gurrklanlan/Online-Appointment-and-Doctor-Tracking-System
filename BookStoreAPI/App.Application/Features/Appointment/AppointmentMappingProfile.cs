using App.Application.Features.Appointment.Create;
using App.Application.Features.Appointment.Update;
using App.Application.Features.User.Update;
using AutoMapper;

namespace App.Application.Features.Appointment
{
    public class AppointmentMappingProfile : Profile
    {
        public AppointmentMappingProfile()
        {
            CreateMap<App.Domain.Entites.Appointment, AppointmentDto>().ReverseMap();


            CreateMap<CreateAppointmentRequest, App.Domain.Entites.Appointment>();

            CreateMap<UpdateAppointmentRequest, App.Domain.Entites.Appointment>()
               .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
 