using Microsoft.AspNetCore.Mvc;

namespace QLNH.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
