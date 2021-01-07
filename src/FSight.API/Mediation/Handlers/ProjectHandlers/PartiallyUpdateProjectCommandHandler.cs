using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FSight.API.Dtos.Project;
using FSight.API.Errors;
using FSight.API.Mediation.Commands.ProjectCommands;
using FSight.Core.Entities;
using FSight.Core.Entities.Identity;
using FSight.Core.Interfaces;
using FSight.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Mediation.Handlers.ProjectHandlers
{
    public class PartiallyUpdateProjectCommandHandler : IRequestHandler<PartiallyUpdateProjectCommand, IActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public PartiallyUpdateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        
        public async Task<IActionResult> Handle(PartiallyUpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var spec = new ProjectsWithMembersSpecification(request.ProjectId);

            var project = await _unitOfWork.Repository<Project>().GetEntityWithSpecification(spec);

            if (project == null)
            {
                return request.Controller.NotFound(new ApiResponse(404));
            }
            
            var projectToPatch = _mapper.Map<ProjectForUpdateDto>(project);
            
            request.PatchDocument.ApplyTo(projectToPatch, request.Controller.ModelState);

            if (!request.Controller.TryValidateModel(projectToPatch))
            {
                return request.Controller.ValidationProblem(request.Controller.ModelState);
            }
            
            var updatedProject = _mapper.Map(projectToPatch, project);
            
            // Ugly way of adding/removing users to project using only 'id' in Json Patch document.
            if (updatedProject.Members.Count > 0)
            {
                var tmp = new List<AppUser>();
                //project.Members.ToList().ForEach(m => tmp.Add(m));
                foreach (var member in projectToPatch.Members)
                {
                    var user = await _userManager.FindByIdAsync(member.Id.ToString());
                    tmp.Add(user);
                }
            
                updatedProject.Members = new List<AppUser>(tmp);
            }

            try
            {
                _unitOfWork.Repository<Project>().Update(updatedProject);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new ApiException(500, ex.Message));
            }

            return request.Controller.NoContent();
        }
    }
}