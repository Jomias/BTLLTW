using Microsoft.AspNetCore.Mvc;
using QLNH.Entities;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class AGalleryController : ControllerBase
	{
		private readonly IGalleryRepository _galleryRepository;

		public AGalleryController(IGalleryRepository galleryRepository)
		{
			_galleryRepository = galleryRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				return Ok(await _galleryRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var gallery = await _galleryRepository.GetAsync(id);
			return gallery == null ? NotFound() : Ok(gallery);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] GalleryModel model)
		{
			if (!ModelState.IsValid) 
			{
				return BadRequest();
            }
			try
			{
				var galleryId = await _galleryRepository.AddAsync(model);
				var gallery = await _galleryRepository.GetAsync(galleryId);
				return gallery == null ? NotFound() : Ok(gallery);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] GalleryModel model)
		{
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
			{
				await _galleryRepository.UpdateAsync(model);
                return Ok(new GalleryModel());
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
				await _galleryRepository.DeleteAsync(id);
				return Ok(new GalleryModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
