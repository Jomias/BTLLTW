using AutoMapper;
using QLNH.Entities;
using QLNH.Models.ViewModels;
using QLNH.Models;
using QLNH.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using QLNH.Helpers;
using AppManager.Utils;

namespace QLNH.Repositories
{
	public class IngredientRepository : IIngredientRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;
		private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "Ingredient";

        public IngredientRepository(QlnhContext context, IMapper mapper, IFileStorageService fileStorageService)
		{
			_context = context;
			_mapper = mapper;
			_fileStorageService = fileStorageService;
		}

		public async Task<long> AddAsync(IngredientModel model)
		{
            if (model.Slug == null)
            {
                model.Slug = Slug.ToUrlSlug(model.Name);
            }
            if (model.AvatarFile != null)
            {
                model.Avatar = await _fileStorageService.SaveFile(containerName, model.AvatarFile);
            }
            var newIngredient = _mapper.Map<Ingredient>(model);
            _context.Ingredients!.Add(newIngredient);
			await _context.SaveChangesAsync();
			return newIngredient.Id;
		}

		public async Task DeleteAsync(long id)
		{
			var deleteIngredient = await _context.Ingredients!.SingleOrDefaultAsync(x => x.Id == id);
			if (deleteIngredient != null)
			{
				deleteIngredient.IsDeleted = true;
				deleteIngredient.UpdatedAt = DateTime.Now;
                _context.Ingredients!.Update(deleteIngredient);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<IngredientModel>> GetAllAsync()
		{
			var Ingredients = await _context.Ingredients!.Where(x => x.IsDeleted == false).ToListAsync();
			return _mapper.Map<List<IngredientModel>>(Ingredients);
		}

		public async Task<IngredientModel> GetAsync(long id)
		{
			var Ingredient = await _context.Ingredients!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<IngredientModel>(Ingredient);
		}

		public async Task<List<IngredientViewModel>> GetAllIngredientViewModel(long? categoryId)
		{
			var temp = await (from a in _context.Ingredients
							 join b in _context.Categories on a.CategoryId equals b.Id
							 where a.IsDeleted == false && b.IsDeleted == false
							 where categoryId == null || a.CategoryId == categoryId
							 select new IngredientViewModel()
							 {
								 Id = a.Id,
								 Name = a.Name,
								 Slug = a.Slug,
								 Quantity = a.Quantity,
								 Summary = a.Summary,
								 Price = a.Price,
								 Unit = a.Unit,
								 Avatar = a.Avatar,
								 Category = b.Name,
							 }).ToListAsync();
			return temp;
		}

		public async Task UpdateAsync(IngredientModel model)
		{
			model.UpdatedAt = DateTime.Now;
            if (model.Slug == null)
            {
                model.Slug = Slug.ToUrlSlug(model.Name);
            }
            if (model.AvatarFile != null)
            {
                model.Avatar = await _fileStorageService.EditFile(containerName, model.AvatarFile, model.Avatar);
            }
            var updateIngredient = _mapper.Map<Ingredient>(model);
            _context.Ingredients!.Update(updateIngredient);
			await _context.SaveChangesAsync();
		}
	}
}
