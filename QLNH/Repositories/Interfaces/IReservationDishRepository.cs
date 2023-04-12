using QLNH.Models;
using QLNH.Models.ViewModels;

namespace QLNH.Repositories.Interfaces
{
	public interface IReservationDishRepository
	{
		public Task<ReservationDishModel> GetAsync(long id);
		public Task<long> AddAsync(ReservationDishModel model);
		public Task UpdateAsync(ReservationDishModel model);
		public Task DeleteAsync(long id);
		public Task<List<ReservationDishModel>> GetAllAsync();
		public Task<List<ReservationDishViewModel>> GetAllReservationDishViewModel(long? reservationId);
	}
}
