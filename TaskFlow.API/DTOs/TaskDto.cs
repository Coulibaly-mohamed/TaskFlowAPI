using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskFlow.API.Models;

namespace TaskFlow.API.DTOs
{
    public class TaskDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public TaskFlow.API.Models.TaskStatus Status { get; set; } // import  casse bonbon car compilareur erreur (nom déjà utilisé par le compilateur)

        public DateTime? DueDate { get; set; }

        public List<string>? Comments { get; set; } = new List<string>();

        [Required]
        public int ProjectId { get; set; }
    }
}
