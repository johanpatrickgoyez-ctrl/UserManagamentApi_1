using System.ComponentModel.DataAnnotations;

namespace UserManagamentApi_1.DTOs
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}