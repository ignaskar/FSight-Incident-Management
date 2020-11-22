using AutoMapper;
using FSight.API.Dtos;
using FSight.Core.Entities;

namespace FSight.API.Profiles
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            CreateMap<Comment, CommentDto>();
        }
    }
}