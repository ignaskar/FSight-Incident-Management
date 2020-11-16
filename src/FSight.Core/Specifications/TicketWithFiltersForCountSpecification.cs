using FSight.Core.Entities;

namespace FSight.Core.Specifications
{
    public class TicketWithFiltersForCountSpecification : BaseSpecification<Ticket>
    {
        public TicketWithFiltersForCountSpecification(TicketSpecParams ticketParams)
            : base(x => 
                (string.IsNullOrEmpty(ticketParams.Search) || x.Number.ToLower().Contains(ticketParams.Search)
                                                           || x.Title.ToLower().Contains(ticketParams.Search)
                                                           || x.Description.ToLower().Contains(ticketParams.Search)))
        {
            
        }
    }
}