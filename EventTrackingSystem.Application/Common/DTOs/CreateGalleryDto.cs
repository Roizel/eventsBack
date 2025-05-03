using Microsoft.AspNetCore.Http;

namespace EventTrackingSystem.Application.Common.DTOs
{
    public class CreateGalleryDto
    {
        public string Title { get; set; } = null!;
        public IFormFile PreviewPhoto { get; set; } = null!;
        public IEnumerable<IFormFile> Photos { get; set; } = new List<IFormFile>();
    }
}
