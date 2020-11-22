using AutoMapper;
using FSight.API.Dtos;
using FSight.Core.Entities.Identity;

namespace FSight.API.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<AppUser, DeveloperDto>();
            CreateMap<AppUser, CustomerDto>();
        }
    }
}