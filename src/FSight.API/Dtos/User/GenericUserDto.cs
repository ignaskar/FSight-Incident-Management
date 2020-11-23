using System;

namespace FSight.API.Dtos.User
{
    public class GenericUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual string EmployeeNumber { get; set; }
    }
}