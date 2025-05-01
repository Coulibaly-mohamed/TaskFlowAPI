using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;
using TaskFlow.API.Services.Interfaces;

namespace TaskFlow.API.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly TaskFlowDbContext _context;

        public ProjectService(TaskFlowDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects.Include(p => p.Tasks).ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> CreateAsync(ProjectCreateDto dto, int userId)
        {
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                CreationDate = DateTime.UtcNow,
                UserId = userId
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> UpdateAsync(int id, ProjectUpdateDto dto, int userId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            if (project == null) return null;

            project.Name = dto.Name;
            project.Description = dto.Description;
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
