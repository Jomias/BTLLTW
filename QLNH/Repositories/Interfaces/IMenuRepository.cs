using QLNH.Models;

namespace QLNH.Repositories.Interfaces
{
	public interface IMenuRepository
	{
		public Task<MenuModel> GetAsync(long id);
		public Task<long> AddAsync(MenuModel model);
		public Task UpdateAsync(MenuModel model);
		public Task DeleteAsync(long id);
		public Task<List<MenuModel>> GetAllAsync();
	}
}
