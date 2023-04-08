using Microsoft.AspNetCore.Mvc;

namespace QLNH.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
