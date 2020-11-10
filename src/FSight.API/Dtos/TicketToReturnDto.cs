using System.Collections.Generic;
using FSight.Core.Enums;

namespace FSight.API.Dtos
{
    public class TicketToReturnDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public ICollection<CommentToReturnDto> Comments { get; set; }
        public DeveloperToReturnDto Assignee { get; set; }
        public CustomerToReturnDto Submitter { get; set; }
    }
}