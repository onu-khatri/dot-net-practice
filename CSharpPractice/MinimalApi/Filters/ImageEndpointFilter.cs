using System.Reflection;

namespace MinimalApi.Filters
{
    // Add EndpointFilterFactory to validate the paths of ImageEndpoints.MapImageEndpoints
    public static class ImageEndpointFilter
    {
        public static EndpointFilterDelegate ValidateStringArgumentFactory(EndpointFilterFactoryContext context, EndpointFilterDelegate next)
        {
            ParameterInfo? pathParameter = context.MethodInfo.GetParameters().FirstOrDefault(p => p.ParameterType == typeof(string));
            if (pathParameter == null)
            {
                //throw new InvalidOperationException("No string parameter found.");
            }

            return async invocationContext =>
            {
                var path = invocationContext.Arguments[pathParameter.Position] as string;
                if (string.IsNullOrEmpty(path))
                {
                    return Results.BadRequest($"Invalid image {pathParameter.Name}.");
                }
                return await next(invocationContext);
            };
        }
    }
}
