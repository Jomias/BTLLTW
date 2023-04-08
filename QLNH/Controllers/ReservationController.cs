using Microsoft.AspNetCore.Mvc;

namespace QLNH.Controllers
{
	public class ReservationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
