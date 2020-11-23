using FSight.Core.Entities;

namespace FSight.Core.Specifications
{
    public class ProjectsWithMembersSpecification : BaseSpecification<Project>
    {
        public ProjectsWithMembersSpecification(ProjectSpecParams projectParams)
            : base (x =>
                (string.IsNullOrEmpty(projectParams.Search) || x.Name.ToLower().Contains(projectParams.Search)))
        {
            AddInclude(x => x.Members);
            
            AddOrderBy(x => x.Name);
            ApplyPaging(projectParams.PageSize * (projectParams.PageIndex - 1), projectParams.PageSize);

            if (!string.IsNullOrEmpty(projectParams.Search))
            {
                switch (projectParams.Sort)
                {
                    case "idAsc":
                        AddOrderBy(x => x.Id);
                        break;
                    case "idDesc":
                        AddOrderByDescending(x => x.Id);
                        break;
                    default:
                        AddOrderBy(x => x.CreateDate);
                        break;
                }
            }
        }

        public ProjectsWithMembersSpecification(int id) : base (x => x.Id == id)
        {
            AddInclude(x => x.Members);
        }
    }
}