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
			IEnumerable<DeleteUser> users = _dbService.GetAllUserDetails();
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
				IEnumerable<DeleteUser> allUsers = _dbService.GetAllUserDetails();
				IEnumerable<DeleteUser> users = allUsers
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

				bool success = _dbService.AddUserToDbWithRole(model);

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
			EditUser model = new()
			{
				FirstName = "firstname",
				LastName = "lastname",
				Email = "email@mail.com",
				Password = "password",
				ConfirmPassword = "",
				Role = "user"
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(EditUser model)
		{
			if (ModelState.IsValid)
			{
				if (true/*check if email already exists*/)
				{
					ModelState.AddModelError("EmailAlreadyExists", "Email is already being used");
					return View(model);
				}
				//update database

				return RedirectToAction("Index");
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult Delete(int? id)
		{
			if (id is null)
			{
				return View("Index");
			}

			DeleteUser model = new()
			{
				Id = 1,
				FirstName = "firstname",
				LastName = "lastname",
				Email = "email@mail.com",
				Password = "password",
				Role = "user"
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			Console.WriteLine(id);
			//delete record with given id

			return RedirectToAction("Index");
		}
	}
}
