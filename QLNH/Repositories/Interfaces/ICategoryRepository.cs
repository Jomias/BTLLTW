using QLNH.Models;

namespace QLNH.Repositories.Interfaces
{
	public interface ICategoryRepository
	{
		public Task<CategoryModel> GetAsync(long id);
		public Task<long> AddAsync(CategoryModel model);
		public Task UpdateAsync(CategoryModel model);
		public Task DeleteAsync(long id);
		public Task<List<CategoryModel>> GetAllAsync();
	}
}
