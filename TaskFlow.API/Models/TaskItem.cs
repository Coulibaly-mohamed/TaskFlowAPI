using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlow.API.Models
{
    public enum TaskStatus { ÀFaire, EnCours, Terminé }

    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public TaskStatus Status { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public DateTime? DueDate { get; set; }

        public ICollection<string> Comments { get; set; } = new List<string>();
    }
}
