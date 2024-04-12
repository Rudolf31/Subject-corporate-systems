using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Corp4Sem4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
			return View();
		}
    }
}
