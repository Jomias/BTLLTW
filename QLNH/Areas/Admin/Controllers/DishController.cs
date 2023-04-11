using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLNH.Entities;
using QLNH.Models;

namespace QLNH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DishController : Controller
    {
        QlnhContext _dbContext = new();
        private readonly IWebHostEnvironment _env;
        public DishController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddDish()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddDish(DishModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult EditDish(long Id)
        {
            var dish = _dbContext.Dishes.Find(Id);
            if (dish == null)
            {
                return NotFound();
            }
            return View(new DishModel()
            {
                Id = dish.Id,
                Name = dish.Name,
                Slug = dish.Slug,
                Price = dish.Price,
                Unit = dish.Unit,
                Summary = dish.Summary,
                Content = dish.Content,
                Avatar = dish.Avatar,
                CreatedBy = dish.CreatedBy,
                CreatedAt = dish.CreatedAt,
                UpdatedAt = dish.UpdatedAt,
                UpdatedBy = dish.UpdatedBy,
                Status = dish.Status,
                IsDeleted = dish.IsDeleted,
            });
        }
        [HttpPost]
        public IActionResult EditDish(DishModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Recipe(long dishId = 0)
        {
            ViewBag.dishId = dishId;
            var name = _dbContext.Dishes.Find(dishId).Name;
            ViewBag.Name = name;
            return View();
        }
        [HttpPost]
        public ActionResult UploadImage(List<IFormFile> files)
        {
            var filePath = "";
            foreach (IFormFile file in Request.Form.Files)
            {
                var fileName = Path.GetFileName(file.FileName);
                var uniqueFileName = GetUniqueFileName(fileName);
                var path = Path.Combine(_env.WebRootPath, "Uploads", uniqueFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                filePath = "/Uploads/" + uniqueFileName;
            }
            return Json(new { url = filePath });
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 8)
                   + Path.GetExtension(fileName);
        }
    }
}
