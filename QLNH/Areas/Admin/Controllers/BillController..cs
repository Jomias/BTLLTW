using Microsoft.AspNetCore.Mvc;
using QLNH.Entities;
using QLNH.Models;

namespace QLNH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillController : Controller
    {
        QlnhContext _dbContext = new ();
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Detail(long reservationId = 0, long billId = 0)
        {
            if (billId > 0)
            {
                ViewBag.Id = billId;
                return View();
            }
            var Id = _dbContext.Bills.FirstOrDefault(x => x.ReservationId == reservationId).Id;
            ViewBag.Id = Id;
            return View();
        }

    }
}
