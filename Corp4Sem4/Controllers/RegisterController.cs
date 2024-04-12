using Corp4Sem4.Database;
using Corpa4Sem4.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Corp4Sem4.Models;

namespace Corp4Sem4.Controllers
{
    public class RegisterController : Controller
	{
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			using (ApplicationContext db = new ApplicationContext())
			{
				var user = new User
				{
					Login = model.Login,
					FullName = model.FullName,
					Password = model.Password
				};

				if (db.Users.Any(u => u.Login == model.Login))
				{
					ViewData["Message"] = "������� ��� ��� ����� ���������������";
					return View();
				}

				await db.Users.AddAsync(user);
				await db.SaveChangesAsync();

				return RedirectToAction("Office", "Office");

			}
		}
	}
}
