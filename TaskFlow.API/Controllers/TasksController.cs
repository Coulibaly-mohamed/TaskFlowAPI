using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        // GET: /api/tasks
        // POST: /api/tasks
        // GET: /api/tasks/{id}
        // PUT: /api/tasks/{id}
        // DELETE: /api/tasks/{id}
    }
}
