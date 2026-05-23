using MinimalApi.Models;
using System.Collections.Concurrent;

namespace MinimalApi.Services;

internal sealed class ImageService
{
    private readonly ConcurrentDictionary<string, StoredImage> _images = new(StringComparer.OrdinalIgnoreCase);

    public string[] GetAllNames()
    {
        return _images.Keys.OrderBy(static name => name).ToArray();
    }

    public StoredImage? GetByName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return _images.TryGetValue(name, out var image) ? image : null;
    }

    public async Task<ServiceResult<ImageMetadata>> AddAsync(IFormFile file, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(file);

        if (file.Length == 0)
        {
            return ServiceResult<ImageMetadata>.ValidationError("Uploaded file is empty.");
        }

        if (_images.ContainsKey(file.FileName))
        {
            return ServiceResult<ImageMetadata>.Conflict($"Image '{file.FileName}' already exists.");
        }

        await using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, cancellationToken);

        var contentType = string.IsNullOrWhiteSpace(file.ContentType)
            ? "application/octet-stream"
            : file.ContentType;

        var image = new StoredImage(file.FileName, contentType, memoryStream.ToArray());
        _images[file.FileName] = image;

        return ServiceResult<ImageMetadata>.Success(new ImageMetadata(image.Name, image.ContentType, image.Content.Length));
    }

    public async Task<ServiceResult<ImageMetadata>> UpdateAsync(string name, IFormFile file, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(file);

        if (!_images.ContainsKey(name))
        {
            return ServiceResult<ImageMetadata>.NotFound($"Image '{name}' was not found.");
        }

        if (file.Length == 0)
        {
            return ServiceResult<ImageMetadata>.ValidationError("Uploaded file is empty.");
        }

        await using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, cancellationToken);

        var contentType = string.IsNullOrWhiteSpace(file.ContentType)
            ? "application/octet-stream"
            : file.ContentType;

        var updatedImage = new StoredImage(name, contentType, memoryStream.ToArray());
        _images[name] = updatedImage;

        return ServiceResult<ImageMetadata>.Success(new ImageMetadata(updatedImage.Name, updatedImage.ContentType, updatedImage.Content.Length));
    }

    public bool Delete(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return _images.TryRemove(name, out _);
    }
}
