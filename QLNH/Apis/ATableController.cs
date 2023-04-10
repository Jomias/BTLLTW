using Microsoft.AspNetCore.Mvc;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class ATableController : ControllerBase
	{
		private readonly ITableRepository _tableRepository;

		public ATableController(ITableRepository tableRepository)
		{
			_tableRepository = tableRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				return Ok(await _tableRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var table = await _tableRepository.GetAsync(id);
			return table == null ? NotFound() : Ok(table);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] TableModel model)
		{
			if (!ModelState.IsValid) 
			{
				return BadRequest();
            }
			try
			{
				var tableId = await _tableRepository.AddAsync(model);
				var table = await _tableRepository.GetAsync(tableId);
				return table == null ? NotFound() : Ok(table);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] TableModel model)
		{
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
			{
				await _tableRepository.UpdateAsync(model);
                return Ok(new TableModel());
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
				await _tableRepository.DeleteAsync(id);
				return Ok(new TableModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
