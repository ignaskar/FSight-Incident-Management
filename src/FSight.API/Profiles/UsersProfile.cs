using AutoMapper;
using FSight.API.Dtos;
using FSight.API.Dtos.User;
using FSight.Core.Entities.Identity;

namespace FSight.API.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<AppUser, GenericUserDto>();
            CreateMap<AppUser, DeveloperDto>();
            CreateMap<GenericUserDto, AppUser>();
        }
    }
}