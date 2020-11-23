using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FSight.API.Dtos.User;

namespace FSight.API.Dtos.Project
{
    public class ProjectForManipulationDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Name should be longer than 5 characters.")]
        [MaxLength(50, ErrorMessage = "Name shouldn't be longer than 50 characters.")]
        public string Name { get; set; }

        [Required]
        public Guid ProjectManagerId { get; set; }

        public virtual ICollection<GenericUserDto> Members { get; set; }
    }
}