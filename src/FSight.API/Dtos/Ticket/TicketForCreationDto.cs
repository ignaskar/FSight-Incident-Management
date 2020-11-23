using System;
using System.Collections.Generic;
using FSight.API.Dtos.Comment;

namespace FSight.API.Dtos.Ticket
{
    public class TicketForCreationDto : TicketForManipulationDto
    {
        public override ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreateDate => DateTime.UtcNow;

        public DateTime LastUpdated => DateTime.UtcNow;
    }
}