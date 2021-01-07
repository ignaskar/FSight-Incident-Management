using FSight.API.Dtos.Project;
using FSight.API.Helpers;
using FSight.Core.Specifications;
using MediatR;

namespace FSight.API.Mediation.Queries.ProjectQueries
{
    public class GetAllProjectsQuery : IRequest<Pagination<ProjectDto>>
    {
        public ProjectSpecParams ProjectParameters { get; }

        public GetAllProjectsQuery(ProjectSpecParams projectParameters)
        {
            ProjectParameters = projectParameters;
        }
    }
}