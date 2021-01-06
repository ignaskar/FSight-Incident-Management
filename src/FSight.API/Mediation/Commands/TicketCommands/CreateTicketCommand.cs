using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FSight.API.Dtos.Comment;
using FSight.API.Dtos.Ticket;
using MediatR;

namespace FSight.API.Mediation.Commands.TicketCommands
{
    public class CreateTicketCommand : IRequest<TicketDto>
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Number shouldn't be longer than 30 characters.")]
        public string Number { get; set; }
        
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

        public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
        
        public Guid CreatedBy { get; set; }
        
        public Guid UpdatedBy { get; set; }
        
        public DateTime CreateDate => DateTime.UtcNow;

        public DateTime LastUpdated => DateTime.UtcNow;
    }
}