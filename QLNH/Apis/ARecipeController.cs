using Microsoft.AspNetCore.Mvc;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class ARecipeController : ControllerBase
	{
		private readonly IRecipeRepository _recipeRepository;

		public ARecipeController(IRecipeRepository recipeRepository)
		{
			_recipeRepository = recipeRepository;
		}

		[HttpGet("GetAllRecipeViewModel")]
		public async Task<IActionResult> GetAllRecipeViewModel([FromQuery] long? dishId)
		{
			try
			{
				return Ok(await _recipeRepository.GetAllRecipeViewModel(dishId));
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
				return Ok(await _recipeRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var recipe = await _recipeRepository.GetAsync(id);
			return recipe == null ? NotFound() : Ok(recipe);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] RecipeModel model)
		{
            if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			try
			{
				var recipeId = await _recipeRepository.AddAsync(model);
				var recipe = await _recipeRepository.GetAsync(recipeId);
				return recipe == null ? NotFound() : Ok(recipe);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] RecipeModel model)
		{
            try
            {
				await _recipeRepository.UpdateAsync(model);
				return Ok(new RecipeModel());
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
				await _recipeRepository.DeleteAsync(id);
				return Ok(new RecipeModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
