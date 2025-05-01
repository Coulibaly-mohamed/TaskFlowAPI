using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskStatusEnum = TaskFlow.API.Models.TaskStatus;

namespace TaskFlow.API.DTOs
{
    public class TaskUpdateDto
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public TaskStatusEnum Status { get; set; }

        public DateTime? DueDate { get; set; }

        public List<string>? Comments { get; set; } = new List<string>();

        public int ProjectId { get; set; }
    }
}
