using AppManager.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;
using System.Net.WebSockets;

namespace QLNH.Repositories
{
	public class GalleryRepository : IGalleryRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;
        public GalleryRepository(QlnhContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
			_mapper = mapper;
        }
        public async Task<long> AddAsync(GalleryModel model)
		{
            if (model.Slug == null)
            {
                model.Slug = Slug.ToUrlSlug(model.Name);
            }
            var newGallery = _mapper.Map<Gallery>(model);
			_context.Galleries!.Add(newGallery);
			await _context.SaveChangesAsync();
			return newGallery.Id;
		}

		public async Task DeleteAsync(long id)
		{
			var deleteGallery = await _context.Galleries!.SingleOrDefaultAsync(x => x.Id == id);
			if (deleteGallery != null) { 
				deleteGallery.IsDeleted = true;
				deleteGallery.UpdatedAt = DateTime.Now;
				_context.Galleries!.Update(deleteGallery);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<GalleryModel>> GetAllAsync()
		{
            var galleries = await _context.Galleries!.Where(x => x.IsDeleted == false).ToListAsync();
            return _mapper.Map<List<GalleryModel>>(galleries);
		}

		public async Task<GalleryModel> GetAsync(long id)
		{
            var gallery = await _context.Galleries!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<GalleryModel>(gallery);
		}

		public async Task UpdateAsync(GalleryModel model)
		{
			model.UpdatedAt = DateTime.Now;
			if (model.Slug == null)
			{
                model.Slug = Slug.ToUrlSlug(model.Name);
            }
            var updateGallery = _mapper.Map<Gallery>(model);
			_context.Galleries!.Update(updateGallery);
			await _context.SaveChangesAsync();
		}
	}
}
