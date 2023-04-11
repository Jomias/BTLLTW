using Microsoft.AspNetCore.Mvc;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class ARoleController : ControllerBase
	{
		private readonly IRoleRepository _roleRepository;

		public ARoleController(IRoleRepository roleRepository)
		{
			_roleRepository = roleRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				return Ok(await _roleRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var role = await _roleRepository.GetAsync(id);
			return role == null ? NotFound() : Ok(role);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] RoleModel model)
		{
			if (!ModelState.IsValid) 
			{
				return BadRequest();
            }
			try
			{
				var roleId = await _roleRepository.AddAsync(model);
				var role = await _roleRepository.GetAsync(roleId);
				return role == null ? NotFound() : Ok(role);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] RoleModel model)
		{
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
			{
				await _roleRepository.UpdateAsync(model);
                return Ok(new RoleModel());
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
				await _roleRepository.DeleteAsync(id);
				return Ok(new RoleModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
