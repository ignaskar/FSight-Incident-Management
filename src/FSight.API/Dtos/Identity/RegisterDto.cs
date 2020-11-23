using System.ComponentModel.DataAnnotations;

namespace FSight.API.Dtos.Identity
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [RegularExpression(@"(?=^.{6,50}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$",
            ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 Number, 1 Non-Alphanumeric and at least 6 characters")]
        public string Password { get; set; }
    }
}