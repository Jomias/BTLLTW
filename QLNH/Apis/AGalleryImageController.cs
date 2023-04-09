using Microsoft.AspNetCore.Mvc;
using QLNH.Models;
using QLNH.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNH.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	public class AGalleryImageController : ControllerBase
	{
		private readonly IGalleryImageRepository _galleryImageRepository;

		public AGalleryImageController(IGalleryImageRepository galleryImageRepository)
		{
			_galleryImageRepository = galleryImageRepository;
		}

		[HttpGet("GetAllGalleryImageViewModel")]
		public async Task<IActionResult> GetAllGalleryImageViewModel([FromQuery] long? GalleryId)
		{
			try
			{
				return Ok(await _galleryImageRepository.GetAllGalleryImageViewModel(GalleryId));
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
				return Ok(await _galleryImageRepository.GetAllAsync());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(long id)
		{
			var galleryImage = await _galleryImageRepository.GetAsync(id);
			return galleryImage == null ? NotFound() : Ok(galleryImage);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] GalleryImageModel model)
		{
            if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			try
			{
				var galleryImageId = await _galleryImageRepository.AddAsync(model);
				var galleryImage = await _galleryImageRepository.GetAsync(galleryImageId);
				return galleryImage == null ? NotFound() : Ok(galleryImage);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromForm] GalleryImageModel model)
		{
            
            try
            {
				await _galleryImageRepository.UpdateAsync(model);
				return Ok(new GalleryImageModel());
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
				await _galleryImageRepository.DeleteAsync(id);
				return Ok(new GalleryImageModel());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
