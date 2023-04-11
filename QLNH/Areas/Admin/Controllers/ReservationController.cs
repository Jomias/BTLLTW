using Microsoft.AspNetCore.Mvc;

namespace QLNH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Reservation : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
