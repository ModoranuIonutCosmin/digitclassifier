using System.ComponentModel.DataAnnotations;

namespace Application.Models.Image
{
    public class ImageRequest
    {
        [Required]
        public string Base64Image { get; set; }
    }
}
