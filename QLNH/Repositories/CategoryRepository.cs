using AppManager.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

namespace QLNH.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;
        public CategoryRepository(QlnhContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }
        public async Task<long> AddAsync(CategoryModel model)
		{
            if (model.Slug == null)
            {
                model.Slug = Slug.ToUrlSlug(model.Name);
            }
            var newCategory = _mapper.Map<Category>(model);
			_context.Categories!.Add(newCategory);
			await _context.SaveChangesAsync();
			return newCategory.Id;
		}

		public async Task DeleteAsync(long id)
		{
			var deleteCategory = await _context.Categories!.SingleOrDefaultAsync(x => x.Id == id);
			if (deleteCategory != null) { 
				deleteCategory.IsDeleted = true;
				deleteCategory.UpdatedAt = DateTime.Now;
				_context.Categories!.Update(deleteCategory);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<CategoryModel>> GetAllAsync()
		{
            var categories = await _context.Categories!.Where(x => x.IsDeleted == false).ToListAsync();
            return _mapper.Map<List<CategoryModel>>(categories);
		}

		public async Task<CategoryModel> GetAsync(long id)
		{
            var category = await _context.Categories!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<CategoryModel>(category);
		}

		public async Task UpdateAsync(CategoryModel model)
		{
			model.UpdatedAt = DateTime.Now;
			if (model.Slug == null)
			{
                model.Slug = Slug.ToUrlSlug(model.Name);
            }
            var updateCategory = _mapper.Map<Category>(model);
			_context.Categories!.Update(updateCategory);
			await _context.SaveChangesAsync();
		}
	}
}
