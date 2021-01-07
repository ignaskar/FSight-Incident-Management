using FSight.API.Dtos.Ticket;
using FSight.API.Helpers;
using FSight.Core.Specifications;
using MediatR;

namespace FSight.API.Mediation.Queries.TicketQueries
{
    public class GetAllTicketsQuery : IRequest<Pagination<TicketDto>>
    {
        public TicketSpecParams TicketParameters { get; }

        public GetAllTicketsQuery(TicketSpecParams parameters)
        {
            TicketParameters = parameters;
        }
    }
}