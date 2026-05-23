using MinimalApi.Models;
using MinimalApi.Services;

namespace MinimalApi.Endpoints;

internal static class UserEndpoints
{
    /// <summary>
    /// Maps minimal API endpoints for in-memory user CRUD operations.
    /// </summary>
    /// <param name="app">The route builder instance.</param>
    /// <returns>The route builder instance for chaining.</returns>
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users");

        group.MapGet("", (UserService userService) =>
        {
            var allUsers = userService.GetAll();
            return Results.Ok(allUsers);
        });

        group.MapGet("/{id:guid}", (Guid id, UserService userService) =>
        {
            var user = userService.GetById(id);
            return user is not null
                ? Results.Ok(user)
                : Results.NotFound($"User with id '{id}' was not found.");
        });

        group.MapPost("", (UserRequest request, UserService userService) =>
        {
            var result = userService.Add(request);
            return result.Status switch
            {
                ServiceResultStatus.ValidationError => Results.BadRequest(result.Error),
                ServiceResultStatus.Conflict => Results.Conflict(result.Error),
                ServiceResultStatus.Success => Results.Created($"/api/users/{result.Value!.Id}", result.Value),
                _ => Results.Problem("Unexpected error occurred while creating user.")
            };
        });

        group.MapPut("/{id:guid}", (Guid id, UserRequest request, UserService userService) =>
        {
            var result = userService.Update(id, request);
            return result.Status switch
            {
                ServiceResultStatus.NotFound => Results.NotFound(result.Error),
                ServiceResultStatus.ValidationError => Results.BadRequest(result.Error),
                ServiceResultStatus.Conflict => Results.Conflict(result.Error),
                ServiceResultStatus.Success => Results.Ok(result.Value),
                _ => Results.Problem("Unexpected error occurred while updating user.")
            };
        });

        group.MapDelete("/{id:guid}", (Guid id, UserService userService) =>
        {
            return userService.Delete(id)
                ? Results.NoContent()
                : Results.NotFound($"User with id '{id}' was not found.");
        });

        return app;
    }
}
