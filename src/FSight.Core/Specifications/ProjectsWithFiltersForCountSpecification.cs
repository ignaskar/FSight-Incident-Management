using FSight.Core.Entities;

namespace FSight.Core.Specifications
{
    public class ProjectsWithFiltersForCountSpecification : BaseSpecification<Project>
    {
        public ProjectsWithFiltersForCountSpecification(ProjectSpecParams projectParams)
            : base(x =>
                (string.IsNullOrEmpty(projectParams.Search) || x.Name.ToLower().Contains(projectParams.Search)))
        {
            
        }
    }
}