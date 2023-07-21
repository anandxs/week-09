using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Models;

namespace UserManagementApp.Controllers
{
	public class DashboardController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			// receive data from database
			IEnumerable<ListUser> users = new List<ListUser>()
			{
				new ListUser()
				{
					Id = 1,
					FirstName = "firstname",
					LastName = "firstname",
					Email = "email@email.com",
					Role = "user"
				},
				new ListUser()
				{
					Id = 2,
					FirstName = "last",
					LastName = "first",
					Email = "eml@ail.com",
					Role = "admin"
				}
			};

			return View(users);
		}

		[HttpGet]
		public IActionResult Search()
		{
			// receive data from database
			IEnumerable<ListUser> users = new List<ListUser>()
			{
				new ListUser()
				{
					Id = 1,
					FirstName = "firstname",
					LastName = "firstname",
					Email = "email@email.com",
					Role = "user"
				}
			};

			return View(users);
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
				if (true/*logic to check if email exits already*/)
				{
					ModelState.AddModelError("EmailAlreadyExists", "Email is already being used");
					return View(model);
				}
				//add to database

				return RedirectToAction("Index");
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult Details(int? id)
		{
			DetailsUser model = new DetailsUser()
			{
				Id = 1,
				FirstName = "firstname",
				LastName = "lastname",
				Email = "email@mail.com",
				Password = "password",
				Role = "admin"
			};

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
