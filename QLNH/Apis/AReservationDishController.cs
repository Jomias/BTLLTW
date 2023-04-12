using Microsoft.AspNetCore.Mvc;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class AReservationDishController : ControllerBase
	{
		private readonly IReservationDishRepository _reservationDishRepository;

		public AReservationDishController(IReservationDishRepository reservationDishRepository)
		{
			_reservationDishRepository = reservationDishRepository;
		}

		[HttpGet("GetAllReservationDishViewModel")]
		public async Task<IActionResult> GetAllReservationDishViewModel([FromQuery] long? ReservationId)
		{
			try
			{
				return Ok(await _reservationDishRepository.GetAllReservationDishViewModel(ReservationId));
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
				return Ok(await _reservationDishRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var reservationDish = await _reservationDishRepository.GetAsync(id);
			return reservationDish == null ? NotFound() : Ok(reservationDish);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] ReservationDishModel model)
		{
            if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			try
			{
				var reservationDishId = await _reservationDishRepository.AddAsync(model);
				var reservationDish = await _reservationDishRepository.GetAsync(reservationDishId);
				return reservationDish == null ? NotFound() : Ok(reservationDish);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] ReservationDishModel model)
		{
            try
            {
				await _reservationDishRepository.UpdateAsync(model);
				return Ok(new ReservationDishModel());
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
				await _reservationDishRepository.DeleteAsync(id);
				return Ok(new ReservationDishModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
