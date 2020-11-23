using System;
using System.Collections.Generic;
using FSight.API.Dtos.User;

namespace FSight.API.Dtos.Project
{
    public class ProjectForCreationDto : ProjectForManipulationDto
    {
        public override ICollection<GenericUserDto> Members { get; set; } = new List<GenericUserDto>();
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreateDate => DateTime.UtcNow;
        public DateTime LastUpdated => DateTime.UtcNow;
    }
}