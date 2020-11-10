using System;
using AutoMapper;
using FSight.API.Dtos;
using FSight.Core.Entities;
using FSight.Core.Enums;

namespace FSight.API.Profiles
{
    public class TicketsProfile : Profile
    {
        public TicketsProfile()
        {
            CreateMap<Ticket, TicketToReturnDto>()
                .ForMember(dest => dest.Assignee,
                    opt => opt.MapFrom(src => src.Developer))
                .ForMember(dest => dest.Submitter,
                    opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.Category,
                    opt => opt.MapFrom(src => Enum.GetName(typeof(TicketCategory), src.Category)));
        }
    }
}