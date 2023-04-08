using Microsoft.AspNetCore.Mvc;
using QLNH.Models;

namespace QLNH.Controllers
{
	public class ContactController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(ContactModel model)
		{
			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}
			return View(model);
		}
	}
}
