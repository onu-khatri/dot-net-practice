using Microsoft.AspNetCore.Mvc.Filters;

namespace MinimalApi.Filters
{
    public class SessionDbValidationAsyncAttribute: IAsyncActionFilter
    {
        private readonly ILogger<SessionDbValidationAsyncAttribute> _logger;

        public SessionDbValidationAsyncAttribute(ILogger<SessionDbValidationAsyncAttribute> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("SessionDbValidationAsyncAttribute: OnActionExecuting");
            // Perform session and database validation logic here
            // For example, you can check if the session is valid and if the database connection is available
            // If validation fails, you can set the context.Result to an appropriate result (e.g., UnauthorizedResult)

            var executedContext = await next();

            // You can also perform additional logic after the action has executed, if needed

        }

    }
}
