using System;

namespace FSight.Core.Entities
{
    public class Comment : BaseEntity
    {
        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        public Developer Developer { get; set; }
        public int? DeveloperId { get; set; }
        public ProjectManager ProjectManager { get; set; }
        public int? ProjectManagerId { get; set; }
        public Customer Customer { get; set; }
        public int? CustomerId { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
    }
}