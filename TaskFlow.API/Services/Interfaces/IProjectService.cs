using TaskFlow.API.DTOs;
using TaskFlow.API.Models;

namespace TaskFlow.API.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task<Project> CreateAsync(ProjectCreateDto dto, int userId);
        Task<Project?> UpdateAsync(int id, ProjectUpdateDto dto, int userId);
        Task<bool> DeleteAsync(int id, int userId);
    }
}
