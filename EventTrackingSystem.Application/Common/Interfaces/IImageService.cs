using Microsoft.AspNetCore.Http;

namespace EventTrackingSystem.Application.Common.Interfaces;
public interface IImageService
{
    Task<string> SaveImageFromUrlAsync(string imageUrl);
    Task<string> SaveImageAsync(byte[] bytes);
    Task<string> SaveImageAsync(IFormFile image);
    Task<string> SaveImageAsync(string base64);
    Task<List<string>> SaveImagesAsync(IEnumerable<byte[]> bytesArrays);
    Task<List<string>> SaveImagesAsync(IEnumerable<IFormFile> images);
    Task<byte[]> LoadBytesAsync(string name);
    Task<string> SaveVideoAsync(IFormFile video);
    void DeleteImage(string nameWithFormat);
    void DeleteImageIfExists(string nameWithFormat);
    void DeleteImages(IEnumerable<string> images);
    void DeleteImagesIfExists(IEnumerable<string> images);
    void DeleteVideo(string nameWithFormat);
    bool IsVideoMimeType(IFormFile file);
    bool IsImage(IFormFile file);
    bool IsImageFile(string fileName);
    bool IsVideoFile(string fileName);
    bool IsVideo(IFormFile file);
    string ImagesDir { get; }
}
