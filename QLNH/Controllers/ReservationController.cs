using Microsoft.AspNetCore.Mvc;
using QLNH.Entities;

namespace QLNH.Controllers
{
	public class ReservationController : Controller
	{
		QlnhContext db = new();
		public IActionResult Index()
		{
			return View();
		}
        public IActionResult Detail(long Id)
        {
			ViewBag.reservationId = Id;
            return View();
        }
    }
}
