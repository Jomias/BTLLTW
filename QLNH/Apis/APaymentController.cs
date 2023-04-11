using Microsoft.AspNetCore.Mvc;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class APaymentController : ControllerBase
	{
		private readonly IPaymentRepository _paymentRepository;

		public APaymentController(IPaymentRepository paymentRepository)
		{
			_paymentRepository = paymentRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				return Ok(await _paymentRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var payment = await _paymentRepository.GetAsync(id);
			return payment == null ? NotFound() : Ok(payment);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] PaymentModel model)
		{
			if (!ModelState.IsValid) 
			{
				return BadRequest();
            }
			try
			{
				var paymentId = await _paymentRepository.AddAsync(model);
				var payment = await _paymentRepository.GetAsync(paymentId);
				return payment == null ? NotFound() : Ok(payment);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] PaymentModel model)
		{
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
			{
				await _paymentRepository.UpdateAsync(model);
                return Ok(new PaymentModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(long id)
		{
            try
            {
				await _paymentRepository.DeleteAsync(id);
				return Ok(new PaymentModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
