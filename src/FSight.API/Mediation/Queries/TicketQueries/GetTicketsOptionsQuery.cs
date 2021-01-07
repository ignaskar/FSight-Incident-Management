using FSight.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Mediation.Queries.TicketQueries
{
    public class GetTicketsOptionsQuery : IRequest<IActionResult>
    {
        public TicketsController Controller { get; }

        public GetTicketsOptionsQuery(TicketsController controller)
        {
            Controller = controller;
        }
    }
}