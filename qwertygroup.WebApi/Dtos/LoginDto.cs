using System.ComponentModel.DataAnnotations;

namespace qwertygroup.WebApi.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
            ErrorMessage = "Password must have 1 Uppercase, 1 lowercase, 1 number, 1 non alphanumerical, and at least 6 characters")]
        public string Password { get; set; }
    }
}