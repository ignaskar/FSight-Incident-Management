using System.Collections.Generic;
using FSight.Core.Enums;

namespace FSight.API.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public DeveloperDto Assignee { get; set; }
        public CustomerDto Submitter { get; set; }
    }
}