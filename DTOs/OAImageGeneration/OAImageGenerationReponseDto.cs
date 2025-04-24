using ImaGen_BE.Models.Constants.OAImage;

namespace ImaGen_BE.DTOs.OAImageGeneration
{
    public class OAImageGenerationResponseDto
    {
        public string Base64String { get; set; } = string.Empty;
        public string Size { get; set; } = OAImageSize.Size256;

    }
}
