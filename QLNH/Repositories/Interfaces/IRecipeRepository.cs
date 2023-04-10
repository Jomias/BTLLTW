using QLNH.Models;
using QLNH.Models.ViewModels;

namespace QLNH.Repositories.Interfaces
{
	public interface IRecipeRepository
	{
		public Task<RecipeModel> GetAsync(long id);
		public Task<long> AddAsync(RecipeModel model);
		public Task UpdateAsync(RecipeModel model);
		public Task DeleteAsync(long id);
		public Task<List<RecipeModel>> GetAllAsync();
		public Task<List<RecipeViewModel>> GetAllRecipeViewModel(long? dishId);
	}
}
