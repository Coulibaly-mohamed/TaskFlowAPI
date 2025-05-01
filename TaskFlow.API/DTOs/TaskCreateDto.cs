using System;
using System.ComponentModel.DataAnnotations;
using TaskFlow.API.Models;

namespace TaskFlow.API.DTOs
{
    public class TaskCreateDto
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public TaskFlow.API.Models.TaskStatus Status { get; set; } // import  casse bonbon car compilareur erreur (nom déjà utilisé par le compilateur)
        public DateTime? DueDate { get; set; }

        public int ProjectId { get; set; }
    }
}
