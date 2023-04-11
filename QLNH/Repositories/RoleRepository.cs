using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;
using System.Net.WebSockets;

namespace QLNH.Repositories
{
	public class RoleRepository : IRoleRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;
        public RoleRepository(QlnhContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }
        public async Task<long> AddAsync(RoleModel model)
		{
			var newRole = _mapper.Map<Role>(model);
			_context.Roles!.Add(newRole);
			await _context.SaveChangesAsync();
			return newRole.Id;
		}

		public Task DeleteAsync(long id)
		{
			throw new NotImplementedException();
        }

		public async Task<List<RoleModel>> GetAllAsync()
		{
            var roles = await _context.Roles!.ToListAsync();
            return _mapper.Map<List<RoleModel>>(roles);
		}

		public async Task<RoleModel> GetAsync(long id)
		{
            var role = await _context.Roles!.FindAsync(id);
            return _mapper.Map<RoleModel>(role);
		}

		public async Task UpdateAsync(RoleModel model)
		{
			var updateRole = _mapper.Map<Role>(model);
			_context.Roles!.Update(updateRole);
			await _context.SaveChangesAsync();
		}
	}
}
