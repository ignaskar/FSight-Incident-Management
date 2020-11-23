using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using FSight.Core.Entities.Identity;

namespace FSight.Core.Specifications
{
    public class ProjectSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        
        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public ICollection<AppUser>? Members { get; set; }
        public string Sort { get; set; }

        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value;
        }
    }
}