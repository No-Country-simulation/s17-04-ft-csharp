using JuniorHub.Application.Contracts.Cloudinary;
using JuniorHub.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JuniorHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudinaryController : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService;
        public CloudinaryController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        /// <summary>
        /// Uploads an image file to Cloudinary and returns the URL of the uploaded image.
        /// </summary>
        /// <param name="media">The image file to be uploaded. Must be provided as multipart/form-data.</param>
        /// <returns>A URL of the uploaded image if successful, otherwise a BadRequest with an error message.</returns>
        /// <response code="200">Returns the URL of the uploaded image.</response>
        /// <response code="400">The upload failed, and an error message is returned.</response>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]  // Assuming the URL is a string
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddImage(IFormFile media)
        {
            var result = await _cloudinaryService.UploadMedia(media);

            if (result.Success)
            {
                return Ok(new { Url = result.Data });
            }
            return BadRequest(result);
        }
    }
}
