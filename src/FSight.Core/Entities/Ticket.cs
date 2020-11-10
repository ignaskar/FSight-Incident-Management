using System;
using System.Collections.Generic;
using FSight.Core.Enums;

namespace FSight.Core.Entities
{
    public class Ticket : BaseEntity
    {
        public string Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TicketState State { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketCategory Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Developer Developer { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public int DeveloperId { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}