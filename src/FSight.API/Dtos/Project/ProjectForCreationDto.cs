using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FSight.API.Dtos.User;

namespace FSight.API.Dtos.Project
{
    public class ProjectForCreationDto : ProjectForManipulationDto
    {
        public override ICollection<GenericUserDto> Members { get; set; } = new List<GenericUserDto>();
        
        [Required]
        public Guid CreatedBy { get; set; }
        [Required]
        public Guid UpdatedBy { get; set; }
        public DateTime CreateDate => DateTime.UtcNow;
        public DateTime LastUpdated => DateTime.UtcNow;
    }
}