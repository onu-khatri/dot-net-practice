using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace MinimalApi.Filters
{
    // order of execution
    // OnActionExecuting
    //---Controller Action Method
    // OnActionExecuted
    // OnResultExecuting
    // -- View Rendering / Result Execution (Json)
    // OnResultExecuted
    public class TimerActionFilterAttribute: ActionFilterAttribute, IActionFilter
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch.Start();
            if(!context.HttpContext.Request.Headers.ContainsKey("X-Client-ID"))
            {
               context.Result = new BadRequestObjectResult("Missing X-Client-ID header");
                // setting value to context.Result will short-circuit the action execution and return the specified result immediately, without executing the action method or any subsequent filters.
            }
        }
        

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();
            var elapsed = _stopwatch.Elapsed;
            Console.WriteLine(elapsed.ToString());

            context.HttpContext.Response.Headers["X-Elapsed-Time"] = elapsed.ToString();
            // You can log the elapsed time or do something with it
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
        }

        /// <inheritdoc />
        public override void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
