using QLNH.Models;

namespace QLNH.Repositories.Interfaces
{
	public interface IRoleRepository
	{
		public Task<RoleModel> GetAsync(long id);
		public Task<long> AddAsync(RoleModel model);
		public Task UpdateAsync(RoleModel model);
		public Task DeleteAsync(long id);
		public Task<List<RoleModel>> GetAllAsync();
	}
}
