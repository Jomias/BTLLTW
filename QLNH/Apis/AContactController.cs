using Microsoft.AspNetCore.Mvc;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class AContactController : ControllerBase
	{
		private readonly IContactRepository _contactRepository;

		public AContactController(IContactRepository contactRepository)
		{
			_contactRepository = contactRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				return Ok(await _contactRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var contact = await _contactRepository.GetAsync(id);
			return contact == null ? NotFound() : Ok(contact);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] ContactModel model)
		{
			if (!ModelState.IsValid) 
			{
				return BadRequest();
            }
			try
			{
				var contactId = await _contactRepository.AddAsync(model);
				var contact = await _contactRepository.GetAsync(contactId);
				return contact == null ? NotFound() : Ok(contact);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] ContactModel model)
		{
            
            try
            {
				await _contactRepository.UpdateAsync(model);
				return Ok();
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
				await _contactRepository.DeleteAsync(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
