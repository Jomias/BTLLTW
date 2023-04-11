using Microsoft.AspNetCore.Mvc;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class AOrderController : ControllerBase
	{
		private readonly IOrderRepository _orderRepository;

		public AOrderController(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		[HttpGet("GetAllOrderViewModel")]
		public async Task<IActionResult> GetAllOrderViewModel([FromQuery] long? billId)
		{
			try
			{
				return Ok(await _orderRepository.GetAllOrderViewModel(billId));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				return Ok(await _orderRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var order = await _orderRepository.GetAsync(id);
			return order == null ? NotFound() : Ok(order);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] OrderModel model)
		{
            if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			try
			{
				var orderId = await _orderRepository.AddAsync(model);
				var order = await _orderRepository.GetAsync(orderId);
				return order == null ? NotFound() : Ok(order);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] OrderModel model)
		{
            try
            {
				await _orderRepository.UpdateAsync(model);
				return Ok(new OrderModel());
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
				await _orderRepository.DeleteAsync(id);
				return Ok(new OrderModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
