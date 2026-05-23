namespace MinimalApi.Models;

internal sealed record StoredImage(string Name, string ContentType, byte[] Content);
