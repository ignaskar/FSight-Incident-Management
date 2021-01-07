using AutoMapper;
using FSight.API.Dtos.Project;
using FSight.API.Mediation.Commands.ProjectCommands;
using FSight.Core.Entities;

namespace FSight.API.Profiles
{
    public class ProjectsProfile : Profile
    {
        public ProjectsProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<CreateProjectCommand, Project>();
            CreateMap<ProjectForUpdateDto, Project>();
            CreateMap<Project, ProjectForUpdateDto>();
        }
    }
}