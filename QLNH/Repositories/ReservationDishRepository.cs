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
	public class ReservationDishRepository : IReservationDishRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;

        public ReservationDishRepository(QlnhContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<long> AddAsync(ReservationDishModel model)
		{
            var newReservationDish = _mapper.Map<ReservationDish>(model);
            _context.ReservationDishes!.Add(newReservationDish);
			await _context.SaveChangesAsync();
			return newReservationDish.Id;
		}

		public async Task DeleteAsync(long id)
		{
			var deleteReservationDish = await _context.ReservationDishes!.SingleOrDefaultAsync(x => x.Id == id);
			if (deleteReservationDish != null)
			{
				deleteReservationDish.IsDeleted = true;
				deleteReservationDish.UpdatedAt = DateTime.Now;
                _context.ReservationDishes!.Update(deleteReservationDish);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<ReservationDishModel>> GetAllAsync()
		{
			var ReservationDishes = await _context.ReservationDishes!.Where(x => x.IsDeleted == false).ToListAsync();
			return _mapper.Map<List<ReservationDishModel>>(ReservationDishes);
		}

		public async Task<ReservationDishModel> GetAsync(long id)
		{
			var ReservationDish = await _context.ReservationDishes!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<ReservationDishModel>(ReservationDish);
		}

		public async Task<List<ReservationDishViewModel>> GetAllReservationDishViewModel(long? reservationId)
		{
			var temp = await (from a in _context.Reservations
							 join b in _context.ReservationDishes on a.Id equals b.ReservationId
							 join c in _context.Dishes on b.DishId equals c.Id
							 where a.IsDeleted == false && b.IsDeleted == false && c.IsDeleted == false
							 where reservationId == null || a.Id == reservationId
							 select new ReservationDishViewModel()
							 {
								 Id = b.Id,
								 Dish = c.Name,
								 Quantity = b.Quantity
							 }).ToListAsync();
			return temp;
		}

		public async Task UpdateAsync(ReservationDishModel model)
		{
            var updateReservationDish = _mapper.Map<ReservationDish>(model);
            _context.ReservationDishes!.Update(updateReservationDish);
			await _context.SaveChangesAsync();
		}
	}
}
