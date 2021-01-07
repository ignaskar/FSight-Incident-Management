using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FSight.API.Dtos.Project;
using FSight.API.Dtos.User;
using MediatR;

namespace FSight.API.Mediation.Commands.ProjectCommands
{
    public class CreateProjectCommand : IRequest<ProjectDto>
    {
        [Required]
        [MinLength(5, ErrorMessage = "Name should be longer than 5 characters.")]
        [MaxLength(50, ErrorMessage = "Name shouldn't be longer than 50 characters.")]
        public string Name { get; set; }

        [Required]
        public Guid ProjectManagerId { get; set; }

        public ICollection<GenericUserDto> Members { get; set; } = new List<GenericUserDto>();
        
        [Required]
        public Guid CreatedBy { get; set; }
        
        [Required]
        public Guid UpdatedBy { get; set; }
        
        public DateTime CreateDate => DateTime.UtcNow;
        
        public DateTime LastUpdated => DateTime.UtcNow;
    }
}