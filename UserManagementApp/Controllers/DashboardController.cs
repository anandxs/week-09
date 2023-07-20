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
    }
}
