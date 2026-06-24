
public class ExceptionHandlingMiddleware : IMiddleware
{
    public ExceptionHandlingMiddleware()
    {

    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        //Catch all exceptions and handle them in a centralized way
        try
        {
            if(context.Request.Path.StartsWithSegments("/csvReport"))
            {
                context.Response.ContentType = "text/csv";
                context.Response.StatusCode = StatusCodes.Status200OK;

                await context.Response.WriteAsync("Name, Age, City\nJohn Doe, 30, New York\nJane Smith, 25, Los Angeles\n");
                return;
            }
            
        
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
            throw;
        }
    }
}