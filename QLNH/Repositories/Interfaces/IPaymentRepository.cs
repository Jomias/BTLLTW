using QLNH.Models;

namespace QLNH.Repositories.Interfaces
{
	public interface IPaymentRepository
	{
		public Task<PaymentModel> GetAsync(long id);
		public Task<long> AddAsync(PaymentModel model);
		public Task UpdateAsync(PaymentModel model);
		public Task DeleteAsync(long id);
		public Task<List<PaymentModel>> GetAllAsync();
	}
}
