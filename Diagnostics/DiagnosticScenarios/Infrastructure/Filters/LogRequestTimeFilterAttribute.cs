using DiagnosticScenarios.Infrastructure.Tracing;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace DiagnosticScenarios.Infrastructure.Filters;

public class LogRequestTimeFilterAttribute : ActionFilterAttribute
{
    readonly Stopwatch _stopwatch = new Stopwatch();

    public override void OnActionExecuting(ActionExecutingContext context) => _stopwatch.Start();

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();

        //MinimalEventCounterSource.Log.Request(
            //context.HttpContext.Request.GetDisplayUrl(), _stopwatch.ElapsedMilliseconds);

        ConditionalEventCounterSource.Log.Request(
            context.HttpContext.Request.GetDisplayUrl(), _stopwatch.ElapsedMilliseconds);
    }
}
