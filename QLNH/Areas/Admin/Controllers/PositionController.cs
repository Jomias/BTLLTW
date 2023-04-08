using Microsoft.AspNetCore.Mvc;

namespace QLNH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
