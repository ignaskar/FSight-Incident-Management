using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FSight.API.Dtos.User;

namespace FSight.API.Dtos.Project
{
    public class ProjectForManipulationDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name shouldn't be longer than 50 characters.")]
        public string Name { get; set; }

        public virtual ICollection<GenericUserDto> Members { get; set; }
    }
}