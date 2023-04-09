using AutoMapper;
using QLNH.Entities;
using QLNH.Models.ViewModels;
using QLNH.Models;
using QLNH.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using QLNH.Helpers;

namespace QLNH.Repositories
{
	public class GalleryImageRepository : IGalleryImageRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;
		private readonly IFileStorageService _fileStorageService;
		private readonly string containerName = "GalleryImage";

		public GalleryImageRepository(QlnhContext context, IMapper mapper, IFileStorageService fileStorageService)
		{
			_context = context;
			_mapper = mapper;
			_fileStorageService = fileStorageService;
		}

		public async Task<long> AddAsync(GalleryImageModel model)
		{

			if (model.PathFile != null)
			{
				model.Name = model.PathFile.FileName;
				model.Path = await _fileStorageService.SaveFile(containerName, model.PathFile);
			}
			var newGalleryImage = _mapper.Map<GalleryImage>(model);
			_context.GalleryImages!.Add(newGalleryImage);
			await _context.SaveChangesAsync();
			return newGalleryImage.Id;
		}

		public async Task DeleteAsync(long id)
		{
			var deleteGalleryImage = await _context.GalleryImages!.SingleOrDefaultAsync(x => x.Id == id);
			if (deleteGalleryImage != null)
			{
				deleteGalleryImage.IsDeleted = true;
				deleteGalleryImage.UpdatedAt = DateTime.Now;
				_context.GalleryImages!.Update(deleteGalleryImage);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<GalleryImageModel>> GetAllAsync()
		{
			var GalleryImages = await _context.GalleryImages!.Where(x => x.IsDeleted == false).ToListAsync();
			return _mapper.Map<List<GalleryImageModel>>(GalleryImages);
		}

		public async Task<GalleryImageModel> GetAsync(long id)
		{
			var GalleryImage = await _context.GalleryImages!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
			return _mapper.Map<GalleryImageModel>(GalleryImage);
		}


		public async Task UpdateAsync(GalleryImageModel model)
		{
			model.UpdatedAt = DateTime.Now;
			if (model.PathFile != null)
			{
                model.Name = model.PathFile.FileName;
                model.Path = await _fileStorageService.EditFile(containerName, model.PathFile, model.Path);
			}
			var updateGalleryImage = _mapper.Map<GalleryImage>(model);
			_context.GalleryImages!.Update(updateGalleryImage);
			await _context.SaveChangesAsync();
		}
        public async Task<List<GalleryImageViewModel>> GetAllGalleryImageViewModel(long? galleryId)
        {
            var temp = await (from a in _context.GalleryImages
                              join b in _context.Galleries on a.GalleryId equals b.Id
                              where a.IsDeleted == false && b.IsDeleted == false
                              where galleryId == null || a.GalleryId == galleryId
                              select new GalleryImageViewModel()
                              {
                                  Id = a.Id,
								  Name = a.Name,
								  Path = a.Path,
								  Gallery = b.Name,
								  Slug = b.Slug
                              }).ToListAsync();
            return temp;
        }
    }
}
