using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Models;

namespace UserManagementApp.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginUser user)
		{
			if (ModelState.IsValid)
			{
				if (true/*invalid credentials logic*/)
				{
					ModelState.AddModelError("InvalidCredentials", "Invalid email or password");

					return View(user);
				}

				//sign in logic
				//redirect to home if user
				//redirect to dashboard if admin
			}

			return View(user);
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(RegisterUser user)
		{
			if (ModelState.IsValid)
			{
				if (true/*if email is already being used*/)
				{
					ModelState.AddModelError("EmailAlreadyExists", "Email is already being used");
					return View(user);
				}
				//if not add all values to db

				return RedirectToAction("Login");
			}

			return View(user);
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync("CookieAuth");

			return View("Login");
		}
	}
}
