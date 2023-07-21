using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Models;
using UserManagementApp.Services;

namespace UserManagementApp.Controllers
{
	[Authorize(Roles = "Admin")]
	public class DashboardController : Controller
	{
		private readonly UserDataManagementService _dbService;

        public DashboardController(UserDataManagementService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
		public IActionResult Index()
		{
			IEnumerable<DetailsUser> users = _dbService.GetAllUserDetails();
			return View(users);
		}

		[HttpGet]
		public IActionResult Search()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Search(SearchUser model)
		{
			if (ModelState.IsValid)
			{
				IEnumerable<DetailsUser> allUsers = _dbService.GetAllUserDetails();
				IEnumerable<DetailsUser> users = allUsers
					.Where(x => x.Email!.Contains(model.SearchString));
				model.Users = users;
				return View(model);
			}

			return View();
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreateUser model)
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
					return RedirectToAction("Index");

				//logic if failed to add
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult Details(int? id)
		{
			DetailsUser model = _dbService.GetDetailsById(id);
			return View(model);
		}

		[HttpGet]
		public IActionResult Edit(int? id)
		{
			DetailsUser temp = _dbService.GetDetailsById(id);
			EditUser model = new()
			{
				FirstName = temp.FirstName,
				LastName = temp.LastName,
				Email = temp.Email,
				Password = temp.Password,
				Role = temp.Role
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(EditUser model)
		{
			if (ModelState.IsValid)
			{
				if (_dbService.EmailExistsAlready(model.Email!, model))
				{
					ModelState.AddModelError("EmailAlreadyExists", "Email is already being used");
					return View(model);
				}

				bool success = _dbService.UpdateUser(model);

				if (success)
					return RedirectToAction("Index");

				//logic if failed
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult Delete(int? id)
		{
			DetailsUser model = _dbService.GetDetailsById(id);

			return View(model);
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			bool success = _dbService.DeleteUser(id);
			if (success)
				return RedirectToAction("Index");

			return RedirectToAction("Index");
		}
	}
}
