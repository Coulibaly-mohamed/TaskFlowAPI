using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        // GET: /api/projects
        // POST: /api/projects
        // GET: /api/projects/{id}
        // PUT: /api/projects/{id}
        // DELETE: /api/projects/{id}
    }
}
