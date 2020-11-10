using AutoMapper;
using FSight.API.Dtos;
using FSight.Core.Entities;

namespace FSight.API.Profiles
{
    public class DevelopersProfile : Profile
    {
        public DevelopersProfile()
        {
            CreateMap<Developer, DeveloperDto>();
        }
    }
}