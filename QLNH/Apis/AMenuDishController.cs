using Microsoft.AspNetCore.Mvc;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class AMenuDishController : ControllerBase
	{
		private readonly IMenuDishRepository _menuDishRepository;

		public AMenuDishController(IMenuDishRepository menuDishRepository)
		{
			_menuDishRepository = menuDishRepository;
		}

		[HttpGet("GetAllMenuDishViewModel")]
		public async Task<IActionResult> GetAllMenuDishViewModel([FromQuery] long? MenuId)
		{
			try
			{
				return Ok(await _menuDishRepository.GetAllMenuDishViewModel(MenuId));
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
				return Ok(await _menuDishRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var menuDish = await _menuDishRepository.GetAsync(id);
			return menuDish == null ? NotFound() : Ok(menuDish);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] MenuDishModel model)
		{
            if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			try
			{
				var menuDishId = await _menuDishRepository.AddAsync(model);
				var menuDish = await _menuDishRepository.GetAsync(menuDishId);
				return menuDish == null ? NotFound() : Ok(menuDish);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] MenuDishModel model)
		{
            try
            {
				await _menuDishRepository.UpdateAsync(model);
				return Ok(new MenuDishModel());
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
				await _menuDishRepository.DeleteAsync(id);
				return Ok(new MenuDishModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
