using QLNH.Models;
using QLNH.Models.ViewModels;

namespace QLNH.Repositories.Interfaces
{
	public interface IGalleryImageRepository
	{
		public Task<GalleryImageModel> GetAsync(long id);
		public Task<long> AddAsync(GalleryImageModel model);
		public Task UpdateAsync(GalleryImageModel model);
		public Task DeleteAsync(long id);
		public Task<List<GalleryImageModel>> GetAllAsync();
        public Task<List<GalleryImageViewModel>> GetAllGalleryImageViewModel(long? positionId);
    }
}
