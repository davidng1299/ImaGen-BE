using ImaGen_BE.DTOs.OAImageGeneration;
using ImaGen_BE.DTOs.OAImageSave;
using ImaGen_BE.Models;
using ImaGen_BE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImaGen_BE.Controllers
{
    [Route("api/v1/openai/images")]
    [ApiController]
    public class OAImageController : ControllerBase
    {
        private readonly IOAImageService _imageService;
        public OAImageController(IOAImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<IEnumerable<OAImageGenerationResponseDto>>> GenerateImage(OAImageGenerationRequestDto requestDto)
        {
            var response = await _imageService.GenerateImageAsync(requestDto);
            return Ok(response);
        }

        [HttpPost("save")]
        public async Task<ActionResult<IEnumerable<OAImageGenerationResponseDto>>> SaveImage(SaveOAImageRequestDto requestDto)
        {
            var response = await _imageService.SaveImageAsync(requestDto);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<List<Image>>>> GetImages(string userId)
        {
            var response = await _imageService.GetImagesAsync(userId);
            return Ok(response);
        }
    }
}
