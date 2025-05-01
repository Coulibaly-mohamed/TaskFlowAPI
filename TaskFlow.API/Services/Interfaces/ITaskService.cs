using TaskFlow.API.DTOs;
using TaskFlow.API.Models;

namespace TaskFlow.API.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskCreateDto dto);
        Task<TaskItem?> UpdateAsync(int id, TaskUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
