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
	public class MenuDishRepository : IMenuDishRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;

        public MenuDishRepository(QlnhContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<long> AddAsync(MenuDishModel model)
		{
            var newMenuDish = _mapper.Map<MenuDish>(model);
            _context.MenuDishes!.Add(newMenuDish);
			await _context.SaveChangesAsync();
			return newMenuDish.Id;
		}

		public async Task DeleteAsync(long id)
		{
			var deleteMenuDish = await _context.MenuDishes!.SingleOrDefaultAsync(x => x.Id == id);
			if (deleteMenuDish != null)
			{
				deleteMenuDish.IsDeleted = true;
				deleteMenuDish.UpdatedAt = DateTime.Now;
                _context.MenuDishes!.Update(deleteMenuDish);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<MenuDishModel>> GetAllAsync()
		{
			var MenuDishes = await _context.MenuDishes!.Where(x => x.IsDeleted == false).ToListAsync();
			return _mapper.Map<List<MenuDishModel>>(MenuDishes);
		}

		public async Task<MenuDishModel> GetAsync(long id)
		{
			var MenuDish = await _context.MenuDishes!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<MenuDishModel>(MenuDish);
		}

		public async Task<List<MenuDishViewModel>> GetAllMenuDishViewModel(long? menuId)
		{
			var temp = await (from a in _context.Menus
							 join b in _context.MenuDishes on a.Id equals b.MenuId
							 join c in _context.Dishes on b.DishId equals c.Id
							 where a.IsDeleted == false && b.IsDeleted == false && c.IsDeleted == false
							 where menuId == null || a.Id == menuId
							 select new MenuDishViewModel()
							 {
								 Id = b.Id,
								 Dish = c.Name,
							 }).ToListAsync();
			return temp;
		}

		public async Task UpdateAsync(MenuDishModel model)
		{
            var updateMenuDish = _mapper.Map<MenuDish>(model);
            _context.MenuDishes!.Update(updateMenuDish);
			await _context.SaveChangesAsync();
		}
	}
}
