using System;
using AutoMapper;
using FSight.API.Dtos;
using FSight.API.Dtos.Ticket;
using FSight.API.Mediation.Commands.TicketCommands;
using FSight.Core.Entities;

namespace FSight.API.Profiles
{
    public class TicketsProfile : Profile
    {
        public TicketsProfile()
        {
            CreateMap<Ticket, TicketDto>();
            CreateMap<CreateTicketCommand, Ticket>();
            CreateMap<TicketForUpdateDto, Ticket>();
            CreateMap<Ticket, TicketForUpdateDto>();
        }
    }
}