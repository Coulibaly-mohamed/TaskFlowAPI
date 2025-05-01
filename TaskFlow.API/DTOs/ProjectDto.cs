using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.DTOs
{
    public class ProjectDto
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
