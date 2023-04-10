using QLNH.Models;
using QLNH.Models.ViewModels;

namespace QLNH.Repositories.Interfaces
{
	public interface IMenuDishRepository
	{
		public Task<MenuDishModel> GetAsync(long id);
		public Task<long> AddAsync(MenuDishModel model);
		public Task UpdateAsync(MenuDishModel model);
		public Task DeleteAsync(long id);
		public Task<List<MenuDishModel>> GetAllAsync();
		public Task<List<MenuDishViewModel>> GetAllMenuDishViewModel(long? menuId);
	}
}
