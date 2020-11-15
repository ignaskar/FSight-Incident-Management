using System;
using Microsoft.AspNetCore.Identity;

namespace FSight.Core.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public virtual Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public virtual Developer Developer { get; set; }
        public int? DeveloperId { get; set; }
        public virtual ProjectManager ProjectManager { get; set; }
        public int? ProjectManagerId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}