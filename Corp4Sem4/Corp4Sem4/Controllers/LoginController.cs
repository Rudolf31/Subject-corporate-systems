using Corp4Sem4.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Corp4Sem4.Models;

namespace Corp4Sem4.Controllers
{
    public class LoginController : Controller
    {
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginViewModel model)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				var user = (from usr in db.Users
							where usr.Login == model.Login
							where usr.Password == model.Password
							select usr).Take(1).ToList().FirstOrDefault(u => true, null);

				if (user == null)
				{
					ViewData["Message"] = "Невалидные данные";
					return View();
				}

				var cookieOptions = new CookieOptions
				{
					Expires = DateTime.Now.AddDays(30)
				};
				Response.Cookies.Append("Login", user.Login, cookieOptions);
				Response.Cookies.Append("Password", user.Password, cookieOptions);

				
				return RedirectToAction("Office", "Office");

			}
		}
	}
}
