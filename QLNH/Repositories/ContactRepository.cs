using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;
using System.Net.WebSockets;

namespace QLNH.Repositories
{
	public class ContactRepository : IContactRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;
        public ContactRepository(QlnhContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }
        public async Task<long> AddAsync(ContactModel model)
		{
			var newContact = _mapper.Map<Contact>(model);
			_context.Contacts!.Add(newContact);
			await _context.SaveChangesAsync();
			return newContact.Id;
		}

		public async Task DeleteAsync(long id)
		{
			var deleteContact = await _context.Contacts!.SingleOrDefaultAsync(x => x.Id == id);
			if (deleteContact != null) { 
				deleteContact.IsDeleted = true;
				deleteContact.UpdatedAt = DateTime.Now;
				_context.Contacts!.Update(deleteContact);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<ContactModel>> GetAllAsync()
		{
			var contacts = await _context.Contacts!.Where(x => x.IsDeleted == false).ToListAsync();
			return _mapper.Map<List<ContactModel>>(contacts);
		}

		public async Task<ContactModel> GetAsync(long id)
		{
			var contacts = await _context.Contacts!.SingleOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            return _mapper.Map<ContactModel>(contacts);
		}

		public async Task UpdateAsync(ContactModel model)
		{
			model.UpdatedAt = DateTime.Now;
			var updateContact = _mapper.Map<Contact>(model);
			_context.Contacts!.Update(updateContact);
			await _context.SaveChangesAsync();
		}
	}
}
