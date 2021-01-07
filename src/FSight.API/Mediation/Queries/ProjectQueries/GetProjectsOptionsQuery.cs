using FSight.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Mediation.Queries.ProjectQueries
{
    public class GetProjectsOptionsQuery : IRequest<IActionResult>
    {
        public ProjectsController Controller { get; set; }

        public GetProjectsOptionsQuery(ProjectsController controller)
        {
            Controller = controller;
        }
    }
}