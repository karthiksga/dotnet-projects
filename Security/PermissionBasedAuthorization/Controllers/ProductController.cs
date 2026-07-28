using Microsoft.AspNetCore.Mvc;

namespace PermissionBasedAuthorization.Controllers
{
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
}