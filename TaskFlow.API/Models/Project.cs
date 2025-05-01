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
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<TaskItem> Tasks { get; set; }
    }
}
