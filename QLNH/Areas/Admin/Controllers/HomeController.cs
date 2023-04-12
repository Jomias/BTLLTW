using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLNH.Entities;
using QLNH.Models.Authentication;

namespace QLNH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        QlnhContext _dbContext = new();

        public IActionResult Index()
        {
            ViewBag.MenuCount =  _dbContext.Menus.Where(x => x.IsDeleted == false).Count();
            ViewBag.TotalMoney = _dbContext.Bills.Where(x => x.Status == 1).Sum(x => x.Total);
            ViewBag.TotalOrder = _dbContext.Orders.Where(x => x.IsDeleted == false).Count();
            ViewBag.TotalEmployee = _dbContext.Employees.Where(x => x.IsDeleted == false).Count();

            return View();
        }
    }
}
