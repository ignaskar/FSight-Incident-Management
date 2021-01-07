using FSight.API.Controllers;
using FSight.API.Dtos.Project;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Mediation.Commands.ProjectCommands
{
    public class PartiallyUpdateProjectCommand : IRequest<IActionResult>
    {
        public int ProjectId { get; }
        public ProjectsController Controller { get; }
        public JsonPatchDocument<ProjectForUpdateDto> PatchDocument { get; }

        public PartiallyUpdateProjectCommand(int projectId, ProjectsController controller, JsonPatchDocument<ProjectForUpdateDto> patchDocument)
        {
            ProjectId = projectId;
            Controller = controller;
            PatchDocument = patchDocument;
        }
    }
}