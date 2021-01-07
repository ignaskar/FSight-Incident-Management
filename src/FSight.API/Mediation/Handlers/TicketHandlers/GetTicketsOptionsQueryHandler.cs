using System.Threading;
using System.Threading.Tasks;
using FSight.API.Controllers;
using FSight.API.Errors;
using FSight.API.Mediation.Queries.TicketQueries;
using FSight.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Mediation.Handlers.TicketHandlers
{
    public class GetTicketsOptionsQueryHandler : IRequestHandler<GetTicketsOptionsQuery, IActionResult>
    {
        private readonly IHttpResponseAccessor _accessor;

        public GetTicketsOptionsQueryHandler(IHttpResponseAccessor accessor)
        {
            _accessor = accessor;
        }
        
        public async Task<IActionResult> Handle(GetTicketsOptionsQuery request, CancellationToken cancellationToken)
        {
            _accessor.Response.Headers.Add("Allow", "GET,POST,PATCH,HEAD,OPTIONS");
            return request.Controller.Ok(new ApiResponse(200));
        }
    }
}