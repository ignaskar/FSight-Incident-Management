using System;
using AutoMapper;
using FSight.API.Dtos;
using FSight.Core.Entities;

namespace FSight.API.Profiles
{
    public class TicketsProfile : Profile
    {
        public TicketsProfile()
        {
            CreateMap<Ticket, TicketDto>()
                .ForMember(dest => dest.Assignee,
                    opt => opt.MapFrom(src => src.Assignee));

            CreateMap<TicketForCreationDto, Ticket>();
        }
    }
}