using Microsoft.AspNetCore.Mvc;
using MinimalApi.Filters;
using MinimalApi.Models;
using MinimalApi.Services;
using System.Net.NetworkInformation;

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

        ///api/users/data
        group.MapGet("data", (IUserService userService) =>
        {
            var allUsers = userService.GetAll();
            return Results.Ok(allUsers);
        });

        // api/users/12321-232-2332-2332
        group.MapGet("/{id:guid}", (Guid id, [FromServices] IUserService userService) =>
        {
            var user = userService.GetById(id);
            return user is not null
                ? Results.Ok(user)
                : Results.NotFound($"User with id '{id}' was not found.");
        });

        //api/users
        group.MapPost("", ([FromBody] UserRequest request, [FromServices] IUserService userService) =>
        {
            //validate request ThrowIfNull
            ArgumentNullException.ThrowIfNull(request);

            var result = userService.Add(request);
            return result.Status switch
            {
                ServiceResultStatus.ValidationError => Results.BadRequest(result.Error),
                ServiceResultStatus.Conflict => Results.Conflict(result.Error),
                ServiceResultStatus.Success => Results.Created($"/api/users/{result.Value!.Id}", result.Value),
                _ => Results.Problem("Unexpected error occurred while creating user.")
            };
        });

        // We can't use ValidationHelper.ValidateGuidAsync with this endpoint here, because id is 3rd argument, and in ValidationHelper.ValidateGuidAsync we are looking for first argument of type Guid, which is id in this case. But if we add another argument before id, then it will be 4th argument and ValidationHelper.ValidateGuidAsync won't find it and will return bad request for all requests.
        // So we can use EndpointfilterFactory to create a filter that will validate id and add it to this endpoint.
        group.MapPut("/{id:guid}", (UserRequest request, IUserService userService, Guid id) =>
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
        }).AddEndpointFilterFactory(ValidationHelper.ValidateIdFactory);

        // Prefere middlewares over the endpoint filters for cross-cutting concerns like validation, logging, etc. Endpoint filters are more suitable for concerns that are specific to a particular endpoint or group of endpoints or where the functionality relies on endpoint concepts such as IResult or EndpointFilterInvocationContext, while middlewares can be applied globally to all endpoints in the application.
        group.MapDelete("/{id:guid}", (Guid id, IUserService userService) =>
        {
            return userService.Delete(id)
                ? Results.NoContent()
                : Results.NotFound($"User with id '{id}' was not found.");
        })
            .AddEndpointFilter<ValidateUserIdFilter>()
            .AddEndpointFilter(ValidationHelper.ValidateGuidAsync)
            .AddEndpointFilter(async (context, next) =>
            {
                return await next(context);
            } );

        return app;
    }
}
