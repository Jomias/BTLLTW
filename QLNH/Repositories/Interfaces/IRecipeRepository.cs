using QLNH.Models;
using QLNH.Models.ViewModels;

namespace QLNH.Repositories.Interfaces
{
	public interface IOrderRepository
	{
		public Task<OrderModel> GetAsync(long id);
		public Task<long> AddAsync(OrderModel model);
		public Task UpdateAsync(OrderModel model);
		public Task DeleteAsync(long id);
		public Task<List<OrderModel>> GetAllAsync();
		public Task<List<OrderViewModel>> GetAllOrderViewModel(long? billId);
	}
}
