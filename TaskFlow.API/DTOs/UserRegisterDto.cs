using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        public required string Name { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required, MinLength(6)]
        public required string Password { get; set; }
    }
}
