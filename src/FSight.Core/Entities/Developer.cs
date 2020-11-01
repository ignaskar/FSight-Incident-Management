
using System.Collections.Generic;
using FSight.Core.Enums;

namespace FSight.Core.Entities
{
    public class Developer : BaseUser
    {
        public ICollection<Ticket> Tickets { get; set; }
        public override UserType Type => UserType.Developer;
    }
}