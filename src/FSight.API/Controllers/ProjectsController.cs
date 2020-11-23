using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FSight.API.Dtos.Project;
using FSight.API.Errors;
using FSight.API.Helpers;
using FSight.Core.Entities;
using FSight.Core.Interfaces;
using FSight.Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
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
        public async Task<ActionResult<ProjectDto>> CreateProject(ProjectForCreationDto project)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new ApiResponse(400, "Unable to verify user permissions. Please try again."));
            }
            
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
                return StatusCode(500, new ApiException(500, ex.Message, ex.StackTrace));
            }

            var projectToReturn = _mapper.Map<ProjectDto>(projectEntity);
            return CreatedAtRoute("GetProject", new {projectId = projectToReturn.Id}, projectToReturn);
        }
    }
}