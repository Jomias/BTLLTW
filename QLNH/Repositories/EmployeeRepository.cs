using AutoMapper;
using QLNH.Entities;
using QLNH.Models.ViewModels;
using QLNH.Models;
using QLNH.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using QLNH.Helpers;

namespace QLNH.Repositories
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;
		private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "Employee";

        public EmployeeRepository(QlnhContext context, IMapper mapper, IFileStorageService fileStorageService)
		{
			_context = context;
			_mapper = mapper;
			_fileStorageService = fileStorageService;
		}

		public async Task<long> AddAsync(EmployeeModel model)
		{

            if (model.AvatarFile != null)
            {
                model.Avatar = await _fileStorageService.SaveFile(containerName, model.AvatarFile);
            }
            var newEmployee = _mapper.Map<Employee>(model);
            _context.Employees!.Add(newEmployee);
			await _context.SaveChangesAsync();
			return newEmployee.Id;
		}

		public async Task DeleteAsync(long id)
		{
			var deleteEmployee = await _context.Employees!.SingleOrDefaultAsync(x => x.Id == id);
			if (deleteEmployee != null)
			{
				deleteEmployee.IsDeleted = true;
				deleteEmployee.UpdatedAt = DateTime.Now;
                _context.Employees!.Update(deleteEmployee);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<EmployeeModel>> GetAllAsync()
		{
			var Employees = await _context.Employees!.Where(x => x.IsDeleted == false).ToListAsync();
			return _mapper.Map<List<EmployeeModel>>(Employees);
		}

		public async Task<EmployeeModel> GetAsync(long id)
		{
			var Employee = await _context.Employees!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<EmployeeModel>(Employee);
		}

		public async Task<List<EmployeeViewModel>> GetAllEmployeeViewModel(long? positionId)
		{
			var temp = await (from a in _context.Employees
							 join b in _context.Positions on a.PositionId equals b.Id
							 where a.IsDeleted == false && b.IsDeleted == false
							 where positionId == null || a.PositionId == positionId
							 select new EmployeeViewModel()
							 {
								 Id = a.Id,
								 Name = a.FirstName + " " + a.LastName,
								 Position = b.Name,
								 Avatar = a.Avatar,
								 Description = a.Description,
							 }).ToListAsync();
			return temp;
		}

		public async Task UpdateAsync(EmployeeModel model)
		{
			model.UpdatedAt = DateTime.Now;
            if (model.AvatarFile != null)
            {
                model.Avatar = await _fileStorageService.EditFile(containerName, model.AvatarFile, model.Avatar);
            }
            var updateEmployee = _mapper.Map<Employee>(model);
            _context.Employees!.Update(updateEmployee);
			await _context.SaveChangesAsync();
		}
	}
}
