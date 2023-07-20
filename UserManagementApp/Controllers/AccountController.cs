using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace UserManagementApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            Console.WriteLine("hi");

            await HttpContext.SignOutAsync("CookieAuth");

            return View("Login");
        }
    }
}
