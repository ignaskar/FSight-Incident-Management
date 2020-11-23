using System;
using System.Collections.Generic;
using FSight.API.Dtos.User;

namespace FSight.API.Dtos.Project
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid ProjectManagerId { get; set; }
        public ICollection<GenericUserDto> Members { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}