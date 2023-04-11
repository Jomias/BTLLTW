using QLNH.Models;

namespace QLNH.Repositories.Interfaces
{
	public interface IReservationRepository
	{
		public Task<ReservationModel> GetAsync(long id);
		public Task<long> AddAsync(ReservationModel model);
		public Task UpdateAsync(ReservationModel model);
		public Task DeleteAsync(long id);
		public Task<List<ReservationModel>> GetAllAsync();
        public Task<List<ReservationModel>> GetReservationByStatusAsync(int? status);
    }
}
