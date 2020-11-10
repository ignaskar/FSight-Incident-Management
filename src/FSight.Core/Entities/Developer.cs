
using System.Collections.Generic;
using FSight.Core.Enums;

namespace FSight.Core.Entities
{
    public class Developer : BaseUser
    {
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public string EmployeeNumber { get; set; }
        public override UserType Type => UserType.Developer;
    }
}