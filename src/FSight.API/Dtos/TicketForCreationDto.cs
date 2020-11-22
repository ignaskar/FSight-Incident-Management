using System;
using System.Collections.Generic;

namespace FSight.API.Dtos
{
    public class TicketForCreationDto : TicketForManipulationDto
    {
        public override ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public Guid CreatedBy { get; set; }
    }
}