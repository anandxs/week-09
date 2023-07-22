using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserManagementApp.Models;
using UserManagementApp.Services;

namespace UserManagementApp.Controllers
{
	[AllowAnonymous]
	public class AccountController : Controller
	{
		private readonly UserDataManagementService _dbService;

        public AccountController(UserDataManagementService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginUser model)
		{
			if (ModelState.IsValid)
			{
				DetailsUser dbModel = _dbService.CheckCredentials(model);

				if (model.Email != dbModel.Email || model.Password != dbModel.Password)
				{
					ModelState.AddModelError("InvalidCredentials", "Invalid email or password");

					return View(model);
				}

				List<Claim> claims = new()
				{
					new Claim(ClaimTypes.Name, dbModel.FirstName +  " " + dbModel.LastName),
					new Claim(ClaimTypes.Email, dbModel.Email!),
					new Claim(ClaimTypes.Role, dbModel.Role!)
				};
				ClaimsIdentity identity = new ClaimsIdentity(claims, "CookieAuth");
				ClaimsPrincipal principal = new ClaimsPrincipal(identity);

				await HttpContext.SignInAsync(
					"CookieAuth", 
					principal,
					new AuthenticationProperties()
					{
						IsPersistent = true
					});

				if (dbModel.Role is "Admin")
				{
					return RedirectToAction("Index", "Dashboard");
				}

				return RedirectToAction("Index", "Home");
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(RegisterUser model)
		{
			if (ModelState.IsValid)
			{
				if (_dbService.EmailExistsAlready(model.Email!))
				{
					ModelState.AddModelError("EmailAlreadyExists", "Email is already being used");
					return View(model);
				}
				
				bool success = _dbService.AddUserToDb(model);
				if (success)
					return RedirectToAction("Login");
				
				//Logic for failing
			}

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync("CookieAuth");

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
