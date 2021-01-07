using System.Threading;
using System.Threading.Tasks;
using FSight.API.Mediation.Queries.ProjectQueries;
using FSight.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Mediation.Handlers.ProjectHandlers
{
    public class GetProjectsOptionsQueryHandler : IRequestHandler<GetProjectsOptionsQuery, IActionResult>
    {
        private readonly IHttpResponseAccessor _accessor;

        public GetProjectsOptionsQueryHandler(IHttpResponseAccessor accessor)
        {
            _accessor = accessor;
        }
        
        public async Task<IActionResult> Handle(GetProjectsOptionsQuery request, CancellationToken cancellationToken)
        {
            _accessor.Response.Headers.Add("Allow", "GET,POST,PATCH,OPTIONS,HEAD");
            return request.Controller.Ok();
        }
    }
}