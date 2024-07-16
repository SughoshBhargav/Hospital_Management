using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
