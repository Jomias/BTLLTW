using QLNH.Models;
using QLNH.Models.ViewModels;

namespace QLNH.Repositories.Interfaces
{
    public interface IBillRepository
    {
		public Task<BillModel> GetAsync(long id);
		public Task<long> AddAsync(BillModel model);
		public Task UpdateAsync(BillModel model);
		public Task DeleteAsync(long id);
		public Task<List<BillModel>> GetAllAsync();
        //public Task<List<BillViewModel>> GetAllBillViewModel(long? menuId);

    }
}
