using Microsoft.AspNetCore.Mvc;

namespace QLNH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(long Id = 0)
        {
            ViewBag.Id = Id;
            return View();
        }
    }
}
