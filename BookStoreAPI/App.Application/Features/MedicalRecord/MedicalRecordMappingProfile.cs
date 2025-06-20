using App.Application.Features.MedicalRecord.Create;
using App.Application.Features.MedicalRecord.Update;
using App.Application.Features.User.Update;
using AutoMapper;

namespace App.Application.Features.MedicalRecord
{
    public class MedicalRecordMappingProfile:Profile
    {
        public MedicalRecordMappingProfile()
        {
            CreateMap<App.Domain.Entites.MedicalRecord, MedicalRecordDto>().ReverseMap();


            CreateMap<CreateMedicalRecordRequest, App.Domain.Entites.MedicalRecord>();
              

            CreateMap<UpdateMedicalRecordRequest, App.Domain.Entites.MedicalRecord>()
              .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
