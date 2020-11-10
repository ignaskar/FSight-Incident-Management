using System.Collections.Generic;
using FSight.Core.Enums;

namespace FSight.Core.Entities
{
    public class Customer : BaseUser
    {
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public override UserType Type => UserType.Customer;
    }
}