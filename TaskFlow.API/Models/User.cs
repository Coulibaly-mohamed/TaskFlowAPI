using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.Models
{
    public enum UserRole { Admin, User }

    public class User
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
