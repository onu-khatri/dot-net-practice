using MinimalApi.Filters;
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
    /// 

    // what is extension method in c#?
    // An extension method in C# is a static method that allows you to add new functionality to existing types without modifying their source code or creating a new derived type. Extension methods are defined in static classes and are marked with the `this` keyword in the first parameter, which specifies the type they extend.
    public static IEndpointRouteBuilder MapImageEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/images");

        group.MapGet("", (ImageService imageService) =>
        {
            var imageNames = imageService.GetAllNames();
            return Results.Ok(imageNames);
        });

        // example to apply endpoint filter over a group of endpoints
        var imageWithValidations = group.MapGroup("/").AddEndpointFilterFactory(ImageEndpointFilter.ValidateStringArgumentFactory);

        imageWithValidations.MapGet("/{name}", (string name, IImageService imageService) =>
        {
            var image = imageService.GetByName(name);
            return image is not null
                ? Results.File(image.Content, image.ContentType, image.Name)
                : Results.NotFound($"Image '{name}' was not found.");
        });

        imageWithValidations.MapPost("", async (IFormFile file, IImageService imageService, CancellationToken cancellationToken) =>
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

        imageWithValidations.MapPut("/{name}", async (string name, IFormFile file, IImageService imageService, CancellationToken cancellationToken) =>
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

        imageWithValidations.MapDelete("/{name}", (string name, IImageService imageService) =>
        {
            return imageService.Delete(name)
                ? Results.NoContent()
                : Results.NotFound($"Image '{name}' was not found.");
        });

        return app;
    }
}
