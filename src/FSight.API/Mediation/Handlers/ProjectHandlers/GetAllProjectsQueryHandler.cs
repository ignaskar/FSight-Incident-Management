using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FSight.API.Dtos.Project;
using FSight.API.Helpers;
using FSight.API.Mediation.Queries.ProjectQueries;
using FSight.Core.Entities;
using FSight.Core.Interfaces;
using FSight.Core.Specifications;
using MediatR;

namespace FSight.API.Mediation.Handlers.ProjectHandlers
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, Pagination<ProjectDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProjectsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<Pagination<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var spec = new ProjectsWithMembersSpecification(request.ProjectParameters);
            
            var countSpec = new ProjectsWithFiltersForCountSpecification(request.ProjectParameters);

            var projects = await _unitOfWork.Repository<Project>().ListAsync(spec);

            var count = await _unitOfWork.Repository<Project>().CountAsync(countSpec);

            var data = _mapper.Map<IReadOnlyList<Project>, IReadOnlyList<ProjectDto>>(projects);

            return new Pagination<ProjectDto>(request.ProjectParameters.PageIndex, request.ProjectParameters.PageSize, count, data);
        }
    }
}