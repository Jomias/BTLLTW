using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;
using System.Net.WebSockets;

namespace QLNH.Repositories
{
	public class PaymentRepository : IPaymentRepository
	{
		private readonly QlnhContext _context;
		private readonly IMapper _mapper;
        public PaymentRepository(QlnhContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }
        public async Task<long> AddAsync(PaymentModel model)
		{
			var newPayment = _mapper.Map<Payment>(model);
			_context.Payments!.Add(newPayment);
			await _context.SaveChangesAsync();
			return newPayment.Id;
		}

		public Task DeleteAsync(long id)
		{
			throw new NotImplementedException();
        }

		public async Task<List<PaymentModel>> GetAllAsync()
		{
            var payments = await _context.Payments!.ToListAsync();
            return _mapper.Map<List<PaymentModel>>(payments);
		}

		public async Task<PaymentModel> GetAsync(long id)
		{
            var payment = await _context.Payments!.FindAsync(id);
            return _mapper.Map<PaymentModel>(payment);
		}

		public async Task UpdateAsync(PaymentModel model)
		{
			var updatePayment = _mapper.Map<Payment>(model);
			_context.Payments!.Update(updatePayment);
			await _context.SaveChangesAsync();
		}
	}
}
