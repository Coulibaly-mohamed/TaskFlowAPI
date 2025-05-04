using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;
using TaskFlow.API.Services.Interfaces;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim!);
        }

        // GET: /api/projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAll()
        {
            var projects = await _projectService.GetAllAsync();
            return Ok(projects);
        }

        // GET: /api/projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetById(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null) return NotFound();

            return Ok(project);
        }

        // POST: /api/projects
        [HttpPost]
        public async Task<ActionResult<Project>> Create([FromBody] ProjectCreateDto dto)
        {
            var userId = GetUserId();
            var project = await _projectService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

        // PUT: /api/projects/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Project>> Update(int id, [FromBody] ProjectUpdateDto dto)
        {
            var userId = GetUserId();
            var updatedProject = await _projectService.UpdateAsync(id, dto, userId);
            if (updatedProject == null) return NotFound();

            return Ok(updatedProject);
        }

        // DELETE: /api/projects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var result = await _projectService.DeleteAsync(id, userId);
            if (!result) return NotFound();

            return Ok(new { message = "Projet supprimé avec succès." });
        }
    }
}
