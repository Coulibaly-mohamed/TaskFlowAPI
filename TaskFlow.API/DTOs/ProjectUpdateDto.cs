using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.DTOs
{
    public class ProjectUpdateDto
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
