using AutoMapper;
using QLNH.Entities;
using QLNH.Models.ViewModels;
using QLNH.Models;
using QLNH.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using QLNH.Helpers;
using AppManager.Utils;

namespace QLNH.Repositories
{
	public class RecipeRepository : IRecipeRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;

        public RecipeRepository(QlnhContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<long> AddAsync(RecipeModel model)
		{
            var newRecipe = _mapper.Map<Recipe>(model);
            _context.Recipes!.Add(newRecipe);
			await _context.SaveChangesAsync();
			return newRecipe.Id;
		}

		public async Task DeleteAsync(long id)
		{
			var deleteRecipe = await _context.Recipes!.SingleOrDefaultAsync(x => x.Id == id);
			if (deleteRecipe != null)
			{
				deleteRecipe.IsDeleted = true;
				deleteRecipe.UpdatedAt = DateTime.Now;
                _context.Recipes!.Update(deleteRecipe);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<RecipeModel>> GetAllAsync()
		{
			var Recipes = await _context.Recipes!.Where(x => x.IsDeleted == false).ToListAsync();
			return _mapper.Map<List<RecipeModel>>(Recipes);
		}

		public async Task<RecipeModel> GetAsync(long id)
		{
			var Recipe = await _context.Recipes!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<RecipeModel>(Recipe);
		}

		public async Task<List<RecipeViewModel>> GetAllRecipeViewModel(long? dishId)
		{
			var temp = await (from a in _context.Dishes
							 join b in _context.Recipes on a.Id equals b.DishId
							 join c in _context.Ingredients on b.IngredientId equals c.Id
							 where a.IsDeleted == false && b.IsDeleted == false && c.IsDeleted == false
							 where dishId == null || a.Id == dishId
							 select new RecipeViewModel()
							 {
								 Id = b.Id,
								 Ingredient = c.Name,
								 Quantity = b.Quantity,
								 Instructions = b.Instructions,
							 }).ToListAsync();
			return temp;
		}

		public async Task UpdateAsync(RecipeModel model)
		{
            var updateRecipe = _mapper.Map<Recipe>(model);
            _context.Recipes!.Update(updateRecipe);
			await _context.SaveChangesAsync();
		}
	}
}
