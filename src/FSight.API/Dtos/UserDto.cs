using System;

namespace FSight.API.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? EmployeeNumber { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}