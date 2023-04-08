using Microsoft.AspNetCore.Mvc;
using QLNH.Models;

namespace QLNH.Controllers
{
	public class DishController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
