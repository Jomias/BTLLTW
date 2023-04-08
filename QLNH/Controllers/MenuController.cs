using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QLNH.Controllers
{
	public class MenuController : Controller
	{
		// GET: MenuController
		public ActionResult Index()
		{
			return View();
		}
	}
}
