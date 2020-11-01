using System;

namespace FSight.Core.Entities
{
    public class Comment : BaseEntity
    {
        public int TicketId { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
    }
}