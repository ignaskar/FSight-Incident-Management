using System;

namespace FSight.API.Dtos.Ticket
{
    public class TicketForUpdateDto : TicketForManipulationDto
    {
        public Guid UpdatedBy { get; set; }
        public Guid AssigneeId { get; set; }
        public DateTime LastUpdated => DateTime.UtcNow;
    }
}