using System;
using System.Threading;
using FSight.Core.Entities;

namespace FSight.Core.Specifications
{
    public class TicketsWithCommentsAndDevelopersSpecification : BaseSpecification<Ticket>
    {
        public TicketsWithCommentsAndDevelopersSpecification(TicketSpecParams ticketParams)
            : base(x => 
                (string.IsNullOrEmpty(ticketParams.Search) || x.Number.ToLower().Contains(ticketParams.Search)
                                                           || x.Title.ToLower().Contains(ticketParams.Search)
                                                           || x.Description.ToLower().Contains(ticketParams.Search)) &&
                (!ticketParams.AssigneeId.HasValue || x.AssigneeId == ticketParams.AssigneeId))
        {
            AddInclude(x => x.Comments);
            AddInclude(x => x.Assignee);

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

        public TicketsWithCommentsAndDevelopersSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Comments);
            AddInclude(x => x.Assignee);
        }
    }
}