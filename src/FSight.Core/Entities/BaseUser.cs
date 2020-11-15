using System;
using System.Collections.Generic;
using FSight.Core.Enums;

namespace FSight.Core.Entities
{
    public abstract class BaseUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}