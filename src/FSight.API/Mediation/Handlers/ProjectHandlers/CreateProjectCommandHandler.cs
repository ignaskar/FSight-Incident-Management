using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FSight.API.Dtos.Project;
using FSight.API.Exceptions;
using FSight.API.Mediation.Commands.ProjectCommands;
using FSight.Core.Entities;
using FSight.Core.Entities.Identity;
using FSight.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FSight.API.Mediation.Handlers.ProjectHandlers
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserAccessor _accessor;

        public CreateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserAccessor accessor)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }
        
        public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var userId = _accessor.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new HttpAccessorNullUserException("An error occured while obtaining user info. Please try again.");
            }
            
            request.ProjectManagerId = Guid.Parse(userId);
            request.CreatedBy = Guid.Parse(userId);
            request.UpdatedBy = Guid.Parse(userId);

            var projectEntity = _mapper.Map<Project>(request);

            try
            {
                _unitOfWork.Repository<Project>().Add(projectEntity);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }

            return _mapper.Map<ProjectDto>(projectEntity);
        }
    }
}