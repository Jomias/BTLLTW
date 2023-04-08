using QLNH.Models;

namespace QLNH.Repositories.Interfaces
{
	public interface IPositionRepository
	{
		public Task<PositionModel> GetAsync(long id);
		public Task<long> AddAsync(PositionModel model);
		public Task UpdateAsync(PositionModel model);
		public Task DeleteAsync(long id);
		public Task<List<PositionModel>> GetAllAsync();
	}
}
