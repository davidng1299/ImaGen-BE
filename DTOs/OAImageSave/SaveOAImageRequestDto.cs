namespace ImaGen_BE.DTOs.OAImageSave
{
    public class SaveOAImageRequestDto
    {
        public string Base64String { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
    }
}
