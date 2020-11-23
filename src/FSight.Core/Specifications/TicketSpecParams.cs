using System;
using System.Collections.Generic;
using FSight.Core.Entities;

namespace FSight.Core.Specifications
{
    public class TicketSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public ICollection<Ticket>? Comments { get; set; }
        public Guid? AssigneeId { get; set; }
        public string Sort { get; set; }

        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}