using Microsoft.AspNetCore.Mvc;

namespace TestProject.API.Controllers
{
    public class CoreController : ControllerBase
    {
        protected IActionResult EntitiesNotFound()
        {
            return NotFound(new {message = "Entities not found"});
        }
    }
}