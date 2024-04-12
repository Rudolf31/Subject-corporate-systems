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

			var messages = (from msg in db.Messages.ToList()
							where msg.ToId == user.Id
							select msg).ToList()
							.Select(msg => new MessageViewModel
							{
								From = db.Users.ToList().First(u => u.Id == msg.FromId).Login,
								Title = msg.Title,
								Text = msg.Text,
								Date = msg.Date
							}).ToList();

			ViewData["Name"] = user.FullName;
			return View(messages);
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
				ViewData["Message"] = $"������: ���������� '{model.To}' �� ����������";
				return View();
			}

			DateTime currentTime = DateTime.UtcNow;
			db.Messages.Add(new Message
			{
				FromId = user.Id,
				ToId = receiverUser.Id,
				Title = model.Title,
				Text = model.Text,
				Date = DateTime.UtcNow
			});
			db.SaveChanges();

			ViewData["Message"] = "�������: ��������� ����������";
			return RedirectToAction("Office", "Office");
		}

	}
}
