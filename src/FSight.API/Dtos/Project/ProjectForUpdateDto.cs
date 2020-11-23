using System;
using System.ComponentModel.DataAnnotations;

namespace FSight.API.Dtos.Project
{
    public class ProjectForUpdateDto : ProjectForManipulationDto
    {
        [Required]
        public Guid UpdatedBy { get; set; }
        public DateTime LastUpdated => DateTime.UtcNow;
    }
}