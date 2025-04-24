using System.ComponentModel.DataAnnotations;

namespace ImaGen_BE.Models
{
    public class User
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Email{ get; set; } = string.Empty;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
