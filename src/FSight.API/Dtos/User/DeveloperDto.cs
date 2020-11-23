using System;

namespace FSight.API.Dtos.User
{
    public class DeveloperDto : GenericUserDto
    {
        public override string EmployeeNumber { get; set; }
    }
}