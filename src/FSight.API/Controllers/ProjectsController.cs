using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FSight.API.Dtos.Project;
using FSight.API.Errors;
using FSight.API.Helpers;
using FSight.Core.Entities;
using FSight.Core.Entities.Identity;
using FSight.Core.Interfaces;
using FSight.Core.Specifications;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public ProjectsController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pagination<ProjectDto>>> GetAllProjects([FromQuery] ProjectSpecParams parameters)
        {
            var spec = new ProjectsWithMembersSpecification(parameters);
            
            var countSpec = new ProjectsWithFiltersForCountSpecification(parameters);

            var projects = await _unitOfWork.Repository<Project>().ListAsync(spec);

            var count = await _unitOfWork.Repository<Project>().CountAsync(countSpec);

            var data = _mapper.Map<IReadOnlyList<Project>, IReadOnlyList<ProjectDto>>(projects);

            return Ok(new Pagination<ProjectDto>(parameters.PageIndex, parameters.PageSize, count, data));
        }

        [HttpGet("{projectId}", Name = "GetProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectDto>> GetSingleProject(int projectId)
        {
            var spec = new ProjectsWithMembersSpecification(projectId);

            var project = await _unitOfWork.Repository<Project>().GetEntityWithSpecification(spec);

            if (project == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper.Map<Project, ProjectDto>(project));
        }

        [HttpPost]
        [Authorize(Roles = "ProjectManager")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProjectDto),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjectDto>> CreateProject(ProjectForCreationDto project)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new ApiResponse(400, "Unable to verify user permissions. Please try again."));
            }
            
            project.ProjectManagerId = Guid.Parse(userId);
            project.CreatedBy = Guid.Parse(userId);
            project.UpdatedBy = Guid.Parse(userId);

            var projectEntity = _mapper.Map<Project>(project);
            _unitOfWork.Repository<Project>().Add(projectEntity);

            try
            {
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new ApiException(500, ex.Message)); 
            }

            var projectToReturn = _mapper.Map<ProjectDto>(projectEntity);
            return CreatedAtRoute("GetProject", new {projectId = projectToReturn.Id}, projectToReturn);
        }

        [HttpPatch("{projectId}")]
        [Authorize(Roles = "ProjectManager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectForUpdateDto), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> PartiallyUpdateProject(int projectId, JsonPatchDocument<ProjectForUpdateDto> patchDocument)
        {
            var spec = new ProjectsWithMembersSpecification(projectId);

            var project = await _unitOfWork.Repository<Project>().GetEntityWithSpecification(spec);

            if (project == null)
            {
                _unitOfWork.Dispose();
                return NotFound(new ApiResponse(404));
            }
            
            var projectToPatch = _mapper.Map<ProjectForUpdateDto>(project);
            
            patchDocument.ApplyTo(projectToPatch, ModelState);

            if (!TryValidateModel(projectToPatch))
            {
                return ValidationProblem(ModelState);
            }

            // Ugly way of adding/removing users to project using only 'id' in Json Patch document.
            var tmp = new List<AppUser>();
            foreach (var member in projectToPatch.Members)
            {
                var user = await _userManager.FindByIdAsync(member.Id.ToString());
                tmp.Add(user);
            }
            
            var updatedProject = _mapper.Map(projectToPatch, project);
            updatedProject.Members = new List<AppUser>(tmp);
            
            try
            {
                _unitOfWork.Repository<Project>().Update(updatedProject);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new ApiException(500, ex.Message));
            }

            return NoContent();
        }

        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProjectsOptions()
        {
            Request.Headers.Add("Allow", "GET,POST,PATCH,OPTIONS,HEAD");
            return Ok();
        }
    }
}