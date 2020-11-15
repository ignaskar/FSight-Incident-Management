using FSight.Core.Entities.Identity;
using FSight.Core.Enums;

namespace FSight.Core.Entities
{
    public class ProjectManager : BaseUser
    {
        public Project Project { get; set; }
        public virtual AppUser User { get; set; }
        public string EmployeeNumber { get; set; }
        public override UserType Type => UserType.ProjectManager;
    }
}