using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
