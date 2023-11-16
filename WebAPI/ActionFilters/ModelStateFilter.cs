using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.ActionFilters;

public class ModelStateFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            // var inValidKeys = (SerializableError)new UnprocessableEntityObjectResult(context.ModelState).Value!;
            // throw new InvalidModelStateException(inValidKeys);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}