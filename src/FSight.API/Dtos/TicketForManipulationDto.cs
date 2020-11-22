using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime;
using FSight.API.ValidationAttributes;

namespace FSight.API.Dtos
{
    [TicketTitleMustBeDifferentFromDescription(ErrorMessage = 
        "Title must be different from description!")]
    public abstract class TicketForManipulationDto
    {
        [Required]
        public virtual string Number { get; set; }
        
        [Required]
        [MaxLength(75, ErrorMessage = "Title shouldn't be longer than 75 characters.")]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(2000, ErrorMessage = "Description shouldn't be longer than 2000 characters.")]
        public string Description { get; set; }
        
        [Required]
        public string State { get; set; }
        
        [Required]
        public string Priority { get; set; }
        
        [Required]
        public string Category { get; set; }
        
        public virtual ICollection<CommentDto> Comments { get; set; }
    }
}