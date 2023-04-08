using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;
using System.Net.WebSockets;

namespace QLNH.Repositories
{
	public class PositionRepository : IPositionRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;
        public PositionRepository(QlnhContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }
        public async Task<long> AddAsync(PositionModel model)
		{
			var newPosition = _mapper.Map<Position>(model);
			_context.Positions!.Add(newPosition);
			await _context.SaveChangesAsync();
			return newPosition.Id;
		}

		public async Task DeleteAsync(long id)
		{
			var deletePosition = await _context.Positions!.SingleOrDefaultAsync(x => x.Id == id);
			if (deletePosition != null) { 
				deletePosition.IsDeleted = true;
				deletePosition.UpdatedAt = DateTime.Now;
				_context.Positions!.Update(deletePosition);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<PositionModel>> GetAllAsync()
		{
            var positions = await _context.Positions!.Where(x => x.IsDeleted == false).ToListAsync();
            return _mapper.Map<List<PositionModel>>(positions);
		}

		public async Task<PositionModel> GetAsync(long id)
		{
            var position = await _context.Positions!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<PositionModel>(position);
		}

		public async Task UpdateAsync(PositionModel model)
		{
			model.UpdatedAt = DateTime.Now;
			var updatePosition = _mapper.Map<Position>(model);
			_context.Positions!.Update(updatePosition);
			await _context.SaveChangesAsync();
		}
	}
}
