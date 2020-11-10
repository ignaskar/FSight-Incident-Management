using System.Threading;
using FSight.Core.Entities;

namespace FSight.Core.Specifications
{
    public class TicketsWithCommentsDevelopersAndCustomersSpecification : BaseSpecification<Ticket>
    {
        public TicketsWithCommentsDevelopersAndCustomersSpecification(TicketSpecParams ticketParams)
            : base(x => 
                (string.IsNullOrEmpty(ticketParams.Search) || x.Number.ToLower().Contains(ticketParams.Search)
                                                           || x.Title.ToLower().Contains(ticketParams.Search)
                                                           || x.Description.ToLower().Contains(ticketParams.Search)) &&
                (!ticketParams.DeveloperId.HasValue || x.DeveloperId == ticketParams.DeveloperId) &&
                (!ticketParams.CustomerId.HasValue || x.CustomerId == ticketParams.CustomerId))
        {
            AddInclude(x => x.Comments);
            AddInclude(x => x.Customer);
            AddInclude(x => x.Developer);
            AddOrderBy(x => x.Number);
            ApplyPaging(ticketParams.PageSize * (ticketParams.PageIndex - 1), ticketParams.PageIndex);

            if (!string.IsNullOrEmpty(ticketParams.Sort))
            {
                switch (ticketParams.Sort)
                {
                    case "numberAsc":
                        AddOrderBy(x => x.Number);
                        break;
                    case "numberDesc":
                            AddOrderByDescending(x => x.Number);
                            break;
                    default:
                        AddOrderBy(x => x.CreateDate);
                        break;
                }
            }
        }

        public TicketsWithCommentsDevelopersAndCustomersSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Comments);
            AddInclude(x => x.Developer);
            AddInclude(x => x.Customer);
        }
    }
}