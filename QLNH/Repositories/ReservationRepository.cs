using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;
using System.Net.WebSockets;

namespace QLNH.Repositories
{
	public class ReservationRepository : IReservationRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;
        public ReservationRepository(QlnhContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }
        public async Task<long> AddAsync(ReservationModel model)
		{
			var newReservation = _mapper.Map<Reservation>(model);
			_context.Reservations!.Add(newReservation);
			await _context.SaveChangesAsync();
			return newReservation.Id;
		}

		public async Task DeleteAsync(long id)
		{
			var deleteReservation = await _context.Reservations!.SingleOrDefaultAsync(x => x.Id == id);
            var deleteBill = await _context.Bills!.SingleOrDefaultAsync(x => x.ReservationId == id);
			if (deleteBill != null)
			{
                var table = await _context.Tables!.SingleOrDefaultAsync(x => x.Id == deleteBill.TableId);
                if (deleteBill.Status == 0)
                {
                    table.Status = 0;
                    _context.Tables.Update(table);
                    await _context.SaveChangesAsync();
                }
				deleteBill.IsDeleted = true;
				_context.Bills.Update(deleteBill);
				await _context.SaveChangesAsync();
            }
			
            if (deleteReservation != null) { 
				deleteReservation.IsDeleted = true;
				deleteReservation.UpdatedAt = DateTime.Now;
				_context.Reservations!.Update(deleteReservation);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<ReservationModel>> GetAllAsync()
		{
            var reservations = await _context.Reservations!
                .Where(r => r.IsDeleted == false)
				.OrderBy(r => Math.Abs(EF.Functions.DateDiffMillisecond(r.BookingDate, DateTime.Now)))
				.ToListAsync();
            return _mapper.Map<List<ReservationModel>>(reservations);
		}

		public async Task<ReservationModel> GetAsync(long id)
		{
            var reservation = await _context.Reservations!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<ReservationModel>(reservation);
		}

		public async Task UpdateAsync(ReservationModel model)
		{
			model.UpdatedAt = DateTime.Now;
			var updateReservation = _mapper.Map<Reservation>(model);
			_context.Reservations!.Update(updateReservation);
			await _context.SaveChangesAsync();
		}
    }

}
