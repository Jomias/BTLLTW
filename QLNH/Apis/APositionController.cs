using Microsoft.AspNetCore.Mvc;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class APositionController : ControllerBase
	{
		private readonly IPositionRepository _positionRepository;

		public APositionController(IPositionRepository positionRepository)
		{
			_positionRepository = positionRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				return Ok(await _positionRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var position = await _positionRepository.GetAsync(id);
			return position == null ? NotFound() : Ok(position);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] PositionModel model)
		{
			if (!ModelState.IsValid) 
			{
				return BadRequest();
            }
			try
			{
				var positionId = await _positionRepository.AddAsync(model);
				var position = await _positionRepository.GetAsync(positionId);
				return position == null ? NotFound() : Ok(position);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] PositionModel model)
		{
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
			{
				await _positionRepository.UpdateAsync(model);
                return Ok(new PositionModel());
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
				await _positionRepository.DeleteAsync(id);
				return Ok(new PositionModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
