using Microsoft.AspNetCore.Mvc;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class AMenuController : ControllerBase
	{
		private readonly IMenuRepository _menuRepository;

		public AMenuController(IMenuRepository menuRepository)
		{
			_menuRepository = menuRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				return Ok(await _menuRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var menu = await _menuRepository.GetAsync(id);
			return menu == null ? NotFound() : Ok(menu);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] MenuModel model)
		{
			if (!ModelState.IsValid) 
			{
				return BadRequest();
            }
			try
			{
				var menuId = await _menuRepository.AddAsync(model);
				var menu = await _menuRepository.GetAsync(menuId);
				return menu == null ? NotFound() : Ok(menu);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] MenuModel model)
		{
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
			{
				await _menuRepository.UpdateAsync(model);
                return Ok(new MenuModel());
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
				await _menuRepository.DeleteAsync(id);
				return Ok(new MenuModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
