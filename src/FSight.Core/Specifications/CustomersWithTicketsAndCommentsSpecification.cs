using FSight.Core.Entities;

namespace FSight.Core.Specifications
{
    public class CustomersWithTicketsAndCommentsSpecification : BaseSpecification<Customer>
    {
        public CustomersWithTicketsAndCommentsSpecification()
        {
            AddInclude(x => x.Tickets);
            AddInclude(x => x.Comments);
        }
    }
}