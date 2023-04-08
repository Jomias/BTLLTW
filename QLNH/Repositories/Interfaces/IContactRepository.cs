using QLNH.Models;

namespace QLNH.Repositories.Interfaces
{
	public interface IContactRepository
	{
		public Task<ContactModel> GetAsync(long id);
		public Task<long> AddAsync(ContactModel model);
		public Task UpdateAsync(ContactModel model);
		public Task DeleteAsync(long id);
		public Task<List<ContactModel>> GetAllAsync();
	}
}
