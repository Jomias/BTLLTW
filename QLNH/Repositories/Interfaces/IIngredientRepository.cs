using QLNH.Models;
using QLNH.Models.ViewModels;

namespace QLNH.Repositories.Interfaces
{
	public interface IIngredientRepository
	{
		public Task<IngredientModel> GetAsync(long id);
		public Task<long> AddAsync(IngredientModel model);
		public Task UpdateAsync(IngredientModel model);
		public Task DeleteAsync(long id);
		public Task<List<IngredientModel>> GetAllAsync();
		public Task<List<IngredientViewModel>> GetAllIngredientViewModel(long? categoryId);
	}
}
