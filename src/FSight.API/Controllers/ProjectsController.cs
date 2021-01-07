using System;
using System.Threading.Tasks;
using AutoMapper;
using FSight.API.Dtos.Project;
using FSight.API.Errors;
using FSight.API.Helpers;
using FSight.API.Mediation.Commands.ProjectCommands;
using FSight.API.Mediation.Queries.ProjectQueries;
using FSight.Core.Entities.Identity;
using FSight.Core.Interfaces;
using FSight.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pagination<ProjectDto>>> GetAllProjects([FromQuery] ProjectSpecParams parameters)
        {
            var query = new GetAllProjectsQuery(parameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{projectId:int}", Name = "GetProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectDto>> GetSingleProject(int projectId)
        {
            var query = new GetProjectByIdQuery(projectId);
            var result = await _mediator.Send(query);
            return result == null
                ? NotFound(new ApiResponse(404, "Requested project was not found."))
                : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "ProjectManager")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProjectDto),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjectDto>> CreateProject(CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute("GetProject", new {projectId = result.Id}, result);
        }

        [HttpPatch("{projectId:int}")]
        [Authorize(Roles = "ProjectManager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectForUpdateDto), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> PartiallyUpdateProject(int projectId, JsonPatchDocument<ProjectForUpdateDto> patchDocument)
        {
            var command = new PartiallyUpdateProjectCommand(projectId, this, patchDocument);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectsOptions()
        {
            var query = new GetProjectsOptionsQuery(this);
            var result = await _mediator.Send(query);
            return result;
        }
    }
}