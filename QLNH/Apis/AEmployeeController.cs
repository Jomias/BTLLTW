using Microsoft.AspNetCore.Mvc;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class AEmployeeController : ControllerBase
	{
		private readonly IEmployeeRepository _employeeRepository;

		public AEmployeeController(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		[HttpGet("GetAllEmployeeViewModel")]
		public async Task<IActionResult> GetAllEmployeeViewModel([FromQuery] long? positionId)
		{
			try
			{
				return Ok(await _employeeRepository.GetAllEmployeeViewModel(positionId));
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
				return Ok(await _employeeRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var employee = await _employeeRepository.GetAsync(id);
			return employee == null ? NotFound() : Ok(employee);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] EmployeeModel model)
		{
            if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			try
			{
				var employeeId = await _employeeRepository.AddAsync(model);
				var employee = await _employeeRepository.GetAsync(employeeId);
				return employee == null ? NotFound() : Ok(employee);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] EmployeeModel model)
		{
            
            try
            {
				await _employeeRepository.UpdateAsync(model);
				return Ok(new EmployeeModel());
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
				await _employeeRepository.DeleteAsync(id);
				return Ok(new EmployeeModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
