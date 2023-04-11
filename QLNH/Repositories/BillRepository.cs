using AppManager.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QLNH.Entities;
using QLNH.Helpers;
using QLNH.Models;
using QLNH.Models.ViewModels;
using QLNH.Repositories.Interfaces;

namespace QLNH.Repositories
{
	public class BillRepository : IBillRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;
        public BillRepository(QlnhContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<long> AddAsync(BillModel model)
		{
            model.CheckIn = DateTime.Now;
			var table = _context.Tables!.Find(model.TableId);
			table.Status = 1;
			_context.Update(table);
            var reservation = _context.Reservations!.Find(model.ReservationId);
			reservation.Status = 2;
			_context.Update(reservation);
            await _context.SaveChangesAsync();
            var newBill = _mapper.Map<Bill>(model);
			_context.Bills!.Add(newBill);
			await _context.SaveChangesAsync();
			return newBill.Id;
		}

		public async Task DeleteAsync(long id)
		{
            var deleteBill = await _context.Bills!.SingleOrDefaultAsync(x => x.Id == id);
			if (deleteBill != null)
			{
				deleteBill.UpdatedAt = DateTime.Now;
				deleteBill.IsDeleted = true;
				_context.Bills!.Update(deleteBill);
				await _context.SaveChangesAsync();
				var reservation = await _context.Reservations!.SingleOrDefaultAsync(x => x.Id == deleteBill.ReservationId);
				if (reservation != null)
				{
					reservation.IsDeleted = true;
					_context.Reservations.Update(reservation);
					await _context.SaveChangesAsync();
				}
				var table = await _context.Tables!.SingleOrDefaultAsync(x => x.Id == deleteBill.TableId);
				if (table != null)
				{
                    table.Status = 0;
                    _context.Tables.Update(table);
                    await _context.SaveChangesAsync();
                }
                deleteBill.UpdatedAt = DateTime.Now;
                deleteBill.IsDeleted = true;
                _context.Bills!.Update(deleteBill);
                await _context.SaveChangesAsync();
            }
		}

		public async Task<List<BillModel>> GetAllAsync()
		{
			var bills = await _context.Bills!.Where(x => x.IsDeleted == false).ToListAsync();
			return _mapper.Map<List<BillModel>>(bills);
		}

		public async Task<BillModel> GetAsync(long id)
		{
            var bill = await _context.Bills!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<BillModel>(bill);
		}

		public async Task UpdateAsync(BillModel model)
		{
            model.UpdatedAt = DateTime.Now;
			model.CheckOut = DateTime.Now;
			model.Total = model.SubTotal * (100 - 5) / 100;
			model.Status = 1;
			var reservation = _context.Reservations.Find(model.ReservationId);
			reservation.Status = 3;
			_context.Update(reservation);
			await _context.SaveChangesAsync();
			var table = _context.Tables.Find(model.TableId);
			table.Status = 0;
			_context.Update(table);
			await _context.SaveChangesAsync();
			var updateBill = _mapper.Map<Bill>(model);
			_context.Bills!.Update(updateBill);
			await _context.SaveChangesAsync();
		}
        public async Task<List<BillViewModel>> GetAllBillViewModel()
		{
            var temp = await (from a in _context.Bills
                              join b in _context.Tables on a.TableId equals b.Id
                              where a.IsDeleted == false && b.IsDeleted == false
                              select new BillViewModel()
                              {
                                  Id = a.Id,
							      SoBan = b.Number,
								  ReservationId = a.ReservationId,
								  Status = a.Status,
                              }).ToListAsync();
            return temp;
        }
    }
}
