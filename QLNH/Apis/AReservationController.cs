using Microsoft.AspNetCore.Mvc;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class AReservationController : ControllerBase
	{
		private readonly IReservationRepository _reservationRepository;

		public AReservationController(IReservationRepository reservationRepository)
		{
			_reservationRepository = reservationRepository;
		}

		[HttpGet("GetReservationByUsername")]
		public async Task<IActionResult> GetReservationByUsername(string Username)
		{
			try
			{
				return Ok(await _reservationRepository.GetReservationByUsername(Username));
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
				return Ok(await _reservationRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        [HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var reservation = await _reservationRepository.GetAsync(id);
			return reservation == null ? NotFound() : Ok(reservation);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] ReservationModel model)
		{
			if (!ModelState.IsValid) 
			{
				return BadRequest();
            }
			try
			{
				var reservationId = await _reservationRepository.AddAsync(model);
				var reservation = await _reservationRepository.GetAsync(reservationId);
				return reservation == null ? NotFound() : Ok(reservation);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] ReservationModel model)
		{
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
			{
				await _reservationRepository.UpdateAsync(model);
                return Ok(new ReservationModel());
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
				await _reservationRepository.DeleteAsync(id);
				return Ok(new ReservationModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
