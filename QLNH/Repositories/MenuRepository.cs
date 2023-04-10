using AppManager.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

namespace QLNH.Repositories
{
	public class MenuRepository : IMenuRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;
        public MenuRepository(QlnhContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }
        public async Task<long> AddAsync(MenuModel model)
		{
            if (model.Slug == null)
            {
                model.Slug = Slug.ToUrlSlug(model.Name);
            }
            var newMenu = _mapper.Map<Menu>(model);
			_context.Menus!.Add(newMenu);
			await _context.SaveChangesAsync();
			return newMenu.Id;
		}

		public async Task DeleteAsync(long id)
		{
			var deleteMenu = await _context.Menus!.SingleOrDefaultAsync(x => x.Id == id);
			if (deleteMenu != null) { 
				deleteMenu.IsDeleted = true;
				deleteMenu.UpdatedAt = DateTime.Now;
				_context.Menus!.Update(deleteMenu);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<MenuModel>> GetAllAsync()
		{
            var menus = await _context.Menus!.Where(x => x.IsDeleted == false).ToListAsync();
            return _mapper.Map<List<MenuModel>>(menus);
		}

		public async Task<MenuModel> GetAsync(long id)
		{
            var menu = await _context.Menus!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<MenuModel>(menu);
		}

		public async Task UpdateAsync(MenuModel model)
		{
			model.UpdatedAt = DateTime.Now;
			if (model.Slug == null)
			{
                model.Slug = Slug.ToUrlSlug(model.Name);
            }
            var updateMenu = _mapper.Map<Menu>(model);
			_context.Menus!.Update(updateMenu);
			await _context.SaveChangesAsync();
		}
	}
}
