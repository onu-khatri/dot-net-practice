namespace MinimalApi.Endpoints;

// add a class to validate Id and used its static method as AddEndpointFilter to validate Id before executing the endpoint handler.

class ValidateUserIdFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var idArgument = context.Arguments.FirstOrDefault(arg => arg is Guid) as Guid?;
        if (idArgument == null || idArgument == Guid.Empty)
        {
            return Results.BadRequest("Invalid user ID.");
        }
        return await next(context);
    }
}


