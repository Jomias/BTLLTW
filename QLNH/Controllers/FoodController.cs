using Microsoft.AspNetCore.Mvc;

namespace QLNH.Controllers
{
    public class FoodController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(long id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
