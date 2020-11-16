using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FSight.Core.Entities.Identity;
using FSight.Core.Enums;

namespace FSight.Core.Entities
{
    public class Ticket
    {
        [Key]
        public string Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TicketState State { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketCategory Category { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public AppUser Assignee { get; set; }
        public Guid? AssigneeId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}