using FSight.API.Dtos.Ticket;
using MediatR;

namespace FSight.API.Mediation.Queries.TicketQueries
{
    public class GetTicketByIdQuery : IRequest<TicketDto>
    {
        public int TicketId { get; }

        public GetTicketByIdQuery(int ticketId)
        {
            TicketId = ticketId;
        }
    }
}