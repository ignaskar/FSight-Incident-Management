using System;

namespace FSight.API.Dtos
{
    public class TicketForUpdateDto : TicketForManipulationDto
    {
        public Guid AssigneeId { get; set; }
        public DateTime LastUpdated => DateTime.UtcNow;
    }
}