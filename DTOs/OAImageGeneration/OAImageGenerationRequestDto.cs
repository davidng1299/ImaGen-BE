using System.ComponentModel.DataAnnotations;
using ImaGen_BE.Models.Constants.OAImage;

namespace ImaGen_BE.DTOs.OAImageGeneration
{
    public class OAImageGenerationRequestDto: IValidatableObject
    {
        [Required]
        public string Prompt { get; set; } = string.Empty;
        [Required]
        public string Size { get; set; } = OAImageSize.Size1024;
        [Required]
        public string Style { get; set; } = OAImageStyle.Vivid;
        [Required]
        public string Quality { get; set; } = OAImageQuality.Standard;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (!OAImageSize.AllowedValues.Contains(Size))
            {
                results.Add(new ValidationResult($"Invalid image size: '{Size}'."));
            }

            if (!OAImageStyle.AllowedValues.Contains(Style))
            {
                results.Add(new ValidationResult($"Invalid image style: '{Style}'."));
            }
            if (!OAImageQuality.AllowedValues.Contains(Quality))
            {
                results.Add(new ValidationResult($"Invalid image quality: '{Quality}'."));
            }

            return results;
        }
    }
}
