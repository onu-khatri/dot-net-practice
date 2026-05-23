using MinimalApi.Models;
using MinimalApi.Services;

namespace MinimalApi.Endpoints;

internal static class ImageEndpoints
{
    /// <summary>
    /// Maps minimal API endpoints for image CRUD operations.
    /// </summary>
    /// <param name="app">The route builder instance.</param>
    /// <returns>The route builder instance for chaining.</returns>
    public static IEndpointRouteBuilder MapImageEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/images");

        group.MapGet("", (ImageService imageService) =>
        {
            var imageNames = imageService.GetAllNames();
            return Results.Ok(imageNames);
        });

        group.MapGet("/{name}", (string name, ImageService imageService) =>
        {
            var image = imageService.GetByName(name);
            return image is not null
                ? Results.File(image.Content, image.ContentType, image.Name)
                : Results.NotFound($"Image '{name}' was not found.");
        });

        group.MapPost("", async (IFormFile file, ImageService imageService, CancellationToken cancellationToken) =>
        {
            var result = await imageService.AddAsync(file, cancellationToken);
            return result.Status switch
            {
                ServiceResultStatus.ValidationError => Results.BadRequest(result.Error),
                ServiceResultStatus.Conflict => Results.Conflict(result.Error),
                ServiceResultStatus.Success => Results.Created($"/api/images/{result.Value!.Name}", result.Value),
                _ => Results.Problem("Unexpected error occurred while creating image.")
            };
        });

        group.MapPut("/{name}", async (string name, IFormFile file, ImageService imageService, CancellationToken cancellationToken) =>
        {
            var result = await imageService.UpdateAsync(name, file, cancellationToken);
            return result.Status switch
            {
                ServiceResultStatus.NotFound => Results.NotFound(result.Error),
                ServiceResultStatus.ValidationError => Results.BadRequest(result.Error),
                ServiceResultStatus.Success => Results.Ok(result.Value),
                _ => Results.Problem("Unexpected error occurred while updating image.")
            };
        });

        group.MapDelete("/{name}", (string name, ImageService imageService) =>
        {
            return imageService.Delete(name)
                ? Results.NoContent()
                : Results.NotFound($"Image '{name}' was not found.");
        });

        return app;
    }
}
