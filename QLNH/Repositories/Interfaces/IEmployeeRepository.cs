using QLNH.Models;
using QLNH.Models.ViewModels;

namespace QLNH.Repositories.Interfaces
{
	public interface IEmployeeRepository
	{
		public Task<EmployeeModel> GetAsync(long id);
		public Task<long> AddAsync(EmployeeModel model);
		public Task UpdateAsync(EmployeeModel model);
		public Task DeleteAsync(long id);
		public Task<List<EmployeeModel>> GetAllAsync();
		public Task<List<EmployeeViewModel>> GetAllEmployeeViewModel(long? galleryId);
	}
}
