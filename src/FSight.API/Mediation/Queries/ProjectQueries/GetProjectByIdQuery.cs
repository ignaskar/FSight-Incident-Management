using FSight.API.Dtos.Project;
using MediatR;

namespace FSight.API.Mediation.Queries.ProjectQueries
{
    public class GetProjectByIdQuery : IRequest<ProjectDto>
    {
        public int ProjectId { get; }
        
        public GetProjectByIdQuery(int projectId)
        {
            ProjectId = projectId;
        }
    }
}