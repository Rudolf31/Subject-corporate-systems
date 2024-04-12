using Corp4Sem4.Database;
using Corp4Sem4.Models;
using Corpa4Sem4.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Corp4Sem4.Controllers
{
    public class OfficeController : Controller
	{
		public IActionResult Office()
		{
			using ApplicationContext db = new ApplicationContext();
			var user = (from usr in db.Users
						where usr.Login == Request.Cookies["Login"]
						where usr.Password == Request.Cookies["Password"]
						select usr).Take(1).ToList().FirstOrDefault(u => true, null);

			if (user == null)
			{
				return RedirectToAction("Index", "Home");
			}

			ViewData["Name"] = user.FullName;
			return View();
		}


		[HttpPost]
		public IActionResult Office(SendMessage model)
		{
			using ApplicationContext db = new ApplicationContext();
			var user = (from usr in db.Users
						where usr.Login == Request.Cookies["Login"]
						where usr.Password == Request.Cookies["Password"]
						select usr).Take(1).ToList().FirstOrDefault(u => true, null);

			if (user == null)
			{
				return RedirectToAction("Index", "Home");
			}

			var receiverUser = (from usr in db.Users.ToList()
								where usr.Login == model.To
								select usr).Take(1).ToList().FirstOrDefault(u => true, null);

			if (receiverUser == null)
			{
				ViewData["Message"] = $"Ошибка: получатель '{model.To}' не существует";
				return View();
			}

			DateTime currentTime = DateTime.UtcNow;
			db.Messages.Add(new Message
			{
				From = user.Id,
				To = receiverUser.Id,
				Title = model.Title,
				Text = model.Text,
				Date = DateTime.UtcNow
			});
			db.SaveChanges();

			ViewData["Message"] = "Успешно: сообщение отправлено";
			return RedirectToAction("Office", "Office");
		}
	}
}
