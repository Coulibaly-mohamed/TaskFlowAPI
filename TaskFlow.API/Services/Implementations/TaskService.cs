using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;
using TaskFlow.API.Services.Interfaces;

namespace TaskFlow.API.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly TaskFlowDbContext _context;

        public TaskService(TaskFlowDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks.Include(t => t.Project).ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaskItem> CreateAsync(TaskCreateDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Status = dto.Status,
                ProjectId = dto.ProjectId,
                DueDate = dto.DueDate,
                Comments = new List<string>()
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> UpdateAsync(int id, TaskUpdateDto dto)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return null;

            task.Title = dto.Title;
            task.Status = dto.Status;
            task.DueDate = dto.DueDate;
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
