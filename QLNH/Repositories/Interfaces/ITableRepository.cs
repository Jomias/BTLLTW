using QLNH.Models;

namespace QLNH.Repositories.Interfaces
{
	public interface ITableRepository
	{
		public Task<TableModel> GetAsync(long id);
		public Task<long> AddAsync(TableModel model);
		public Task UpdateAsync(TableModel model);
		public Task DeleteAsync(long id);
		public Task<List<TableModel>> GetAllAsync();
	}
}
