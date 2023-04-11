using Microsoft.AspNetCore.Mvc;
using QLNH.Entities;
using QLNH.Models;
using System.Linq;

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
                var item = _dbContext.Bills.Find(billId);
                ViewBag.Id = item.Id;
                ViewBag.Status = item.Status;
                return View();
            }
            var temp = _dbContext.Bills.FirstOrDefault(x => x.ReservationId == reservationId);
            ViewBag.Status = temp.Status;
            ViewBag.Id = temp.Id;
            return View();
        }
        public IActionResult Invoice(long reservationId = 0, long billId = 0)
        {
            if (billId > 0)
            {
                var item = _dbContext.Bills.Find(billId);
                ViewBag.Id = item.Id;
                return View();
            }
            var temp = _dbContext.Bills.FirstOrDefault(x => x.ReservationId == reservationId);
            ViewBag.Id = temp.Id;
            return View();
        }

    }
}
