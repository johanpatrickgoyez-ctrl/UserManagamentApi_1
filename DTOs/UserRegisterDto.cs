using System.ComponentModel.DataAnnotations;

namespace UserManagamentApi_1.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}