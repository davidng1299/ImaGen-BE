using System.ComponentModel.DataAnnotations;

namespace ImaGen_BE.Models
{
    public class Image
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string UserId { get; set; } = string.Empty;
        public string? Url { get; set; } = string.Empty;
        [Required]
        public int Size { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User User { get; set; } = null!;
    }
}
