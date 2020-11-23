using System;
using System.ComponentModel.DataAnnotations;
using FSight.Core.Entities.Identity;

namespace FSight.Core.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        public AppUser Author { get; set; }
        public Guid AuthorId { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
    }
}