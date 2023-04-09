using Microsoft.AspNetCore.Mvc;
using QLNH.Entities;
using QLNH.Models;

namespace QLNH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IngredientController : Controller
    {
        QlnhContext _dbContext = new ();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddIngredient()
        {
            return View();
        }
		[HttpPost]
		public IActionResult AddIngredient(IngredientModel model)
		{
			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}
			return View(model);
		}
        [HttpGet]
        public IActionResult EditIngredient(long Id)
        {
            var ingredient = _dbContext.Ingredients.Find(Id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return View(new IngredientModel()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Slug = ingredient.Slug,
                Price = ingredient.Price,
                Quantity = ingredient.Quantity,
                Unit = ingredient.Unit,
                Summary = ingredient.Summary,
                Avatar = ingredient.Avatar,
                CategoryId = ingredient.CategoryId,
                CreatedBy = ingredient.CreatedBy,
                CreatedAt = ingredient.CreatedAt,
                UpdatedAt = ingredient.UpdatedAt,
                UpdatedBy = ingredient.UpdatedBy,
                Status = ingredient.Status,
                IsDeleted = ingredient.IsDeleted,
            });
        }
        [HttpPost]
        public IActionResult EditIngredient(IngredientModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
