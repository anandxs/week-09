using Microsoft.AspNetCore.Mvc;

namespace UserManagementApp.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }
    }
}
