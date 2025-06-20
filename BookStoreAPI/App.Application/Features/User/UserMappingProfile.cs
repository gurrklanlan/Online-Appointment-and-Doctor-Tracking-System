using App.Application.Features.User.Create;
using App.Application.Features.User.Update;
using AutoMapper;

namespace App.Application.Features.User
{
    public class UserMappingProfile:Profile
    {
        public UserMappingProfile()
        {
            CreateMap<App.Domain.Entites.User, UserDto>().ReverseMap();

            CreateMap<CreateUserRequest, App.Domain.Entites.User>();

            CreateMap<UpdateUserRequest, App.Domain.Entites.User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
