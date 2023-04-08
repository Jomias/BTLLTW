using Microsoft.AspNetCore.Mvc;

namespace QLNH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }
    }
}
