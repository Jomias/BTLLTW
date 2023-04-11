using QLNH.Models;
using QLNH.Models.ViewModels;

namespace QLNH.Repositories.Interfaces
{
    public interface IDishRepository
    {
		public Task<DishModel> GetAsync(long id);
		public Task<long> AddAsync(DishModel model);
		public Task UpdateAsync(DishModel model);
		public Task DeleteAsync(long id);
		public Task<List<DishModel>> GetAllAsync();
		public Task<List<MenuPageDishViewModel>> GetDishByMenuName(string name);
        public Task<List<DishViewModel>> GetAllDishViewModel(long? menuId);
        public Task<List<DishViewModel>> GetAllDishWithRecipeViewModel();
    }
}
