using Microsoft.AspNetCore.Mvc;
using QLNH.Models;
using QLNH.Repositories;
using QLNH.Repositories.Interfaces;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ABillController : ControllerBase
    {
        private readonly IBillRepository _dishRepository;

        public ABillController(IBillRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				return Ok(await _dishRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var dish = await _dishRepository.GetAsync(id);
			return dish == null ? NotFound() : Ok(dish);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] BillModel model)
		{
            if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			try
			{
				var dishId = await _dishRepository.AddAsync(model);
				var dish = await _dishRepository.GetAsync(dishId);
				return dish == null ? NotFound() : Ok(dish);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] BillModel model)
		{
            
            try
            {
				await _dishRepository.UpdateAsync(model);
				return Ok(new BillModel());
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
				await _dishRepository.DeleteAsync(id);
				return Ok(new BillModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
