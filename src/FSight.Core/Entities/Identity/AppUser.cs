using System;
using Microsoft.AspNetCore.Identity;

namespace FSight.Core.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public virtual Customer Customer { get; set; }
        public virtual Developer Developer { get; set; }
        public virtual ProjectManager ProjectManager { get; set; }
    }
}