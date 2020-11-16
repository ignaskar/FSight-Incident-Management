using System;
using System.Collections.Generic;
using FSight.Core.Entities.Identity;

namespace FSight.Core.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<AppUser> Members { get; set; } = new List<AppUser>();
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}