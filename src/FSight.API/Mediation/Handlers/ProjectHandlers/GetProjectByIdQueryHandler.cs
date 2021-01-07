using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FSight.API.Dtos.Project;
using FSight.API.Errors;
using FSight.API.Mediation.Queries.ProjectQueries;
using FSight.Core.Entities;
using FSight.Core.Interfaces;
using FSight.Core.Specifications;
using MediatR;

namespace FSight.API.Mediation.Handlers.ProjectHandlers
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public GetProjectByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new ProjectsWithMembersSpecification(request.ProjectId);

            var project = await _unitOfWork.Repository<Project>().GetEntityWithSpecification(spec);

            return project == null ? null : _mapper.Map<Project, ProjectDto>(project);
        }
    }
}