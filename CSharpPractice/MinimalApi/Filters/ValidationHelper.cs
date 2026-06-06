using System.Reflection;

namespace MinimalApi.Endpoints;

class ValidationHelper
{
    internal static async ValueTask<object?> ValidateGuidAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var id = context.Arguments.FirstOrDefault(arg => arg is Guid) as Guid? ?? Guid.Empty;
        if (id == Guid.Empty)
        {
            return Results.BadRequest("Invalid user ID.");
        }
        return await next(context);
    }

    internal static EndpointFilterDelegate ValidateIdFactory(EndpointFilterFactoryContext context, EndpointFilterDelegate next)
    {
        ParameterInfo? idParameter = context.MethodInfo.GetParameters().FirstOrDefault(p => p.ParameterType == typeof(Guid));
        if (idParameter == null)
        {
            throw new InvalidOperationException("No Guid parameter found.");
        }

        return async invocationContext =>
        {
            var id = invocationContext.Arguments[idParameter.Position] as Guid? ?? Guid.Empty;
            if (id == Guid.Empty)
            {
                return Results.BadRequest("Invalid user ID.");
            }
            return await next(invocationContext);
        };
    }
}


