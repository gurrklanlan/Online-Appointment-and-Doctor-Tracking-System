using App.Application.Features.DoctorDetail.Create;
using App.Application.Features.DoctorDetail.Update;
using App.Application.Features.User.Update;
using AutoMapper;

namespace App.Application.Features.DoctorDetail
{
    public class DoctorDetailMappingProfile:Profile
    {
        public DoctorDetailMappingProfile()
        {
            CreateMap<App.Domain.Entites.User, DoctorDetailDto>().ReverseMap();

            CreateMap<CreateDoctorDetailRequest, App.Domain.Entites.DoctorDetail>();
                

            CreateMap<UpdateDoctorDetailRequest, App.Domain.Entites.DoctorDetail>()
              .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
