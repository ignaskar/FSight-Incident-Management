using AutoMapper;
using FSight.API.Dtos;
using FSight.Core.Entities;

namespace FSight.API.Profiles
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.ProjectManagerName,
                    opt => opt.MapFrom(src => $"{src.ProjectManager.FirstName} {src.ProjectManager.LastName}"))
                .ForMember(dest => dest.CustomerName,
                    opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"))
                .ForMember(dest => dest.DeveloperName,
                    opt => opt.MapFrom(src => $"{src.Developer.FirstName} {src.Developer.LastName}"));
            //.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}