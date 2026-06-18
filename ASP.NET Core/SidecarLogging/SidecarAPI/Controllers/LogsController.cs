using Microsoft.AspNetCore.Mvc;

namespace SidecarAPI.Controllers
{
    public class LogsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
