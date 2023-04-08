using QLNH.Models;

namespace QLNH.Repositories.Interfaces
{
	public interface IGalleryRepository
	{
		public Task<GalleryModel> GetAsync(long id);
		public Task<long> AddAsync(GalleryModel model);
		public Task UpdateAsync(GalleryModel model);
		public Task DeleteAsync(long id);
		public Task<List<GalleryModel>> GetAllAsync();
	}
}
