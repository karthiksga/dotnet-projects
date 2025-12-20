using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TeacherApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        [Authorize(Roles = "Teacher")]
        [HttpGet("teacher-data")]
        public IActionResult GetTeacherData()
        {
            return Ok("This is teacher-only data.");
        }
    }
}
