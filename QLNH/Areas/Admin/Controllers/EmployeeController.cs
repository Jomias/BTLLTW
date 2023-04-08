using Microsoft.AspNetCore.Mvc;
using QLNH.Entities;
using QLNH.Models;

namespace QLNH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        QlnhContext _dbContext = new ();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
		[HttpPost]
		public IActionResult AddEmployee(EmployeeModel model)
		{
			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}
			return View(model);
		}
        [HttpGet]
        public IActionResult EditEmployee(long Id)
        {
            var employee = _dbContext.Employees.Find(Id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(new EmployeeModel()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Description = employee.Description,
                PositionId = employee.PositionId,
                Avatar = employee.Avatar,
                CreatedBy = employee.CreatedBy,
                CreatedAt = employee.CreatedAt,
                UpdatedAt = employee.UpdatedAt,
                UpdatedBy = employee.UpdatedBy,
                Status = employee.Status,
                IsDeleted = employee.IsDeleted,
            });
        }
        [HttpPost]
        public IActionResult EditEmployee(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
