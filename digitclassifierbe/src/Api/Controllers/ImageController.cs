using Application.Models.Image;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Api.Attributes;
using DataAcces.Entities;

namespace Api.Controllers
{
    [Route("api/images/predict")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Predict([FromBody] ImageRequest request)
        {
            User user = (User) HttpContext.Items["User"];
            var ImageResponse = await _imageService.Predict(user, request);
            IActionResult response = ImageResponse == null ? Unauthorized() : Ok(ImageResponse);
            return response;
        }
    }
}
