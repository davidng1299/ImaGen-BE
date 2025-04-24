using ImaGen_BE.DTOs.OAImageGeneration;
using ImaGen_BE.DTOs.OAImageSave;
using ImaGen_BE.Models;

namespace ImaGen_BE.Services
{
    public interface IOAImageService
    {
        Task<OAImageGenerationResponseDto> GenerateImageAsync(OAImageGenerationRequestDto requestDto);
        Task<Image> SaveImageAsync(SaveOAImageRequestDto requestDto);
        Task<List<Image>> GetImagesAsync(string userId);
    }
}
