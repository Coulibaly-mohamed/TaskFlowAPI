using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.DTOs
{
    public class UserLoginDto
    {
        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
