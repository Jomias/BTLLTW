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
			}
		}

		public async Task<List<BillModel>> GetAllAsync()
		{
			var dishs = await _context.Bills!.Where(x => x.IsDeleted == false).ToListAsync();
			return _mapper.Map<List<BillModel>>(dishs);
		}

		public async Task<BillModel> GetAsync(long id)
		{
            var dish = await _context.Bills!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<BillModel>(dish);
		}

		public async Task UpdateAsync(BillModel model)
		{
            model.UpdatedAt = DateTime.Now;
			var updateBill = _mapper.Map<Bill>(model);
			_context.Bills!.Update(updateBill);
			await _context.SaveChangesAsync();
		}

    }
}
