using System;
using System.Collections.Generic;
using FSight.API.Dtos.Comment;
using FSight.API.Dtos.User;

namespace FSight.API.Dtos.Ticket
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
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public DeveloperDto Assignee { get; set; }
    }
}