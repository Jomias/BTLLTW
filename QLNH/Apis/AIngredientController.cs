using Microsoft.AspNetCore.Mvc;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class AIngredientController : ControllerBase
	{
		private readonly IIngredientRepository _ingredientRepository;

		public AIngredientController(IIngredientRepository ingredientRepository)
		{
			_ingredientRepository = ingredientRepository;
		}

		[HttpGet("GetAllIngredientViewModel")]
		public async Task<IActionResult> GetAllIngredientViewModel([FromQuery] long? categoryId)
		{
			try
			{
				return Ok(await _ingredientRepository.GetAllIngredientViewModel(categoryId));
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
				return Ok(await _ingredientRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var ingredient = await _ingredientRepository.GetAsync(id);
			return ingredient == null ? NotFound() : Ok(ingredient);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] IngredientModel model)
		{
            if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			try
			{
				var ingredientId = await _ingredientRepository.AddAsync(model);
				var ingredient = await _ingredientRepository.GetAsync(ingredientId);
				return ingredient == null ? NotFound() : Ok(ingredient);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] IngredientModel model)
		{
            
            try
            {
				await _ingredientRepository.UpdateAsync(model);
				return Ok(new IngredientModel());
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
				await _ingredientRepository.DeleteAsync(id);
				return Ok(new IngredientModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
