using AppManager.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLNH.Entities;
using QLNH.Helpers;
using QLNH.Models;
using QLNH.Models.ViewModels;
using QLNH.Repositories.Interfaces;

namespace QLNH.Repositories
{
	public class DishRepository : IDishRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "Dish";
        public DishRepository(QlnhContext context, IMapper mapper, IFileStorageService fileStorageService)
		{
			_context = context;
			_mapper = mapper;
			_fileStorageService = fileStorageService;
		}

		public async Task<List<MenuPageDishViewModel>> GetDishByMenuName(string name)
		{
			var temp = await (from a in _context.Menus
							  join b in _context.MenuDishes on a.Id equals b.MenuId
							  join c in _context.Dishes on b.DishId equals c.Id
							  where a.IsDeleted == false && b.IsDeleted == false && c.IsDeleted == false
							  where a.Name == name
							  select new MenuPageDishViewModel()
							  {
								  Id = c.Id,
								  Name = c.Name,
								  Price = c.Price,
								  Summary = c.Summary,
								  Avatar = c.Avatar,
							  }).ToListAsync();
			return temp;
		}

		public async Task<long> AddAsync(DishModel model)
		{
            if (model.Slug == null)
            {
                model.Slug = Slug.ToUrlSlug(model.Name);
            }
            if (model.AvatarFile != null)
            {
                model.Avatar = await _fileStorageService.SaveFile(containerName, model.AvatarFile);
            }
            var newDish = _mapper.Map<Dish>(model);
			_context.Dishes!.Add(newDish);
			await _context.SaveChangesAsync();
			return newDish.Id;
		}

		public async Task DeleteAsync(long id)
		{
            var deleteDish = await _context.Dishes!.SingleOrDefaultAsync(x => x.Id == id);
			if (deleteDish != null)
			{
				deleteDish.UpdatedAt = DateTime.Now;
				deleteDish.IsDeleted = true;
				_context.Dishes!.Update(deleteDish);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<DishModel>> GetAllAsync()
		{
			var dishs = await _context.Dishes!.Where(x => x.IsDeleted == false).ToListAsync();
			return _mapper.Map<List<DishModel>>(dishs);
		}

		public async Task<DishModel> GetAsync(long id)
		{
            var dish = await _context.Dishes!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<DishModel>(dish);
		}

		public async Task UpdateAsync(DishModel model)
		{
            if (model.Slug == null)
            {
                model.Slug = Slug.ToUrlSlug(model.Name);
            }
            if (model.AvatarFile != null)
            {
                model.Avatar = await _fileStorageService.SaveFile(containerName, model.AvatarFile);
            }
            model.UpdatedAt = DateTime.Now;
			var updateDish = _mapper.Map<Dish>(model);
			_context.Dishes!.Update(updateDish);
			await _context.SaveChangesAsync();
		}

        public async Task<List<DishViewModel>> GetAllDishViewModel(long? menuId)
        {
            var dishes = await _context.Dishes
                .Where(d => d.IsDeleted == false)
                .Select(d => new DishViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Slug = d.Slug,
                    Price = d.Price,
                    Unit = d.Unit,
                    Summary = d.Summary,
                    Content = d.Content,
                    Instructions = d.Instructions,
                    Avatar = d.Avatar
                })
                .ToListAsync();

            if (menuId != null)
            {
                dishes = dishes
                    .Where(d => _context.MenuDishes
                        .Any(md => md.MenuId == menuId && md.DishId == d.Id && md.IsDeleted == false))
                    .ToList();
            }

            return dishes;

        }

        public async Task<List<DishViewModel>> GetAllDishWithRecipeViewModel()
        {
			var temp = await _context.MenuDishes.Where(x => x.IsDeleted == false).Select(x => x.Id).Distinct().ToListAsync();
            return await _context.Dishes.Where(x => x.IsDeleted == false && temp.Contains(x.Id))
                .Select(d => new DishViewModel()
                {

                    Id = d.Id,
                    Name = d.Name,
                    Slug = d.Slug,
                    Price = d.Price,
                    Unit = d.Unit,
                    Summary = d.Summary,
                    Content = d.Content,
                    Instructions = d.Instructions,
                    Avatar = d.Avatar
                }).ToListAsync();
        }
    }
}
