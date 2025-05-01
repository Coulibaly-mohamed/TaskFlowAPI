using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.DTOs
{
    public class ProjectCreateDto
    {
        [Required]
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
