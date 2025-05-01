using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlow.API.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; } = null!; // Utilisation de null! pour indiquer que cette propriété sera initialisée sans afficher warning cs8618
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>(); // Initialisation de la collection
    }
}
