using MinimalApi.Models;

namespace MinimalApi.Services
{
    internal interface IImageService
    {
        Task<ServiceResult<ImageMetadata>> AddAsync(IFormFile file, CancellationToken cancellationToken);
        bool Delete(string name);
        string[] GetAllNames();
        StoredImage? GetByName(string name);
        Task<ServiceResult<ImageMetadata>> UpdateAsync(string name, IFormFile file, CancellationToken cancellationToken);
    }
}