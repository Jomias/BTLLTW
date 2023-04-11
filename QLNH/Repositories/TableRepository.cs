using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;
using System.Net.WebSockets;

namespace QLNH.Repositories
{
	public class TableRepository : ITableRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;
        public TableRepository(QlnhContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }
        public async Task<long> AddAsync(TableModel model)
		{
			var newTable = _mapper.Map<Table>(model);
			_context.Tables!.Add(newTable);
			await _context.SaveChangesAsync();
			return newTable.Id;
		}

		public async Task DeleteAsync(long id)
		{
			var deleteTable = await _context.Tables!.SingleOrDefaultAsync(x => x.Id == id);
			if (deleteTable != null) { 
				deleteTable.IsDeleted = true;
				deleteTable.UpdatedAt = DateTime.Now;
				_context.Tables!.Update(deleteTable);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<TableModel>> GetAllAsync()
		{
            var tables = await _context.Tables!.Where(x => x.IsDeleted == false).OrderBy(x => x.Number).ToListAsync();
            return _mapper.Map<List<TableModel>>(tables);
		}

		public async Task<TableModel> GetAsync(long id)
		{
            var table = await _context.Tables!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<TableModel>(table);
		}

		public async Task UpdateAsync(TableModel model)
		{
			model.UpdatedAt = DateTime.Now;
			var updateTable = _mapper.Map<Table>(model);
			_context.Tables!.Update(updateTable);
			await _context.SaveChangesAsync();
		}

        public async Task<List<TableModel>> GetTableByStatusAsync(int? status)
		{
            var tables = await _context.Tables!
				.Where(x => x.IsDeleted == false && (status == null || x.Status == status))
				.OrderBy(x => x.Number).ToListAsync();
            return _mapper.Map<List<TableModel>>(tables);
        }
    }
}
