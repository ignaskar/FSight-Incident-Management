using System;
using System.Collections.Generic;

namespace FSight.Core.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Developer> Members { get; set; }
        public ProjectManager ProjectManager { get; set; }
        public int ProjectManagerId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}