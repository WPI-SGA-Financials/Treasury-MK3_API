using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Treasury.WebAPI.Filters.ActionFilters;

public class ValidateInputActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Action Filter Executing");
        var dict = context.ActionArguments;

        if (dict.ContainsKey("fy"))
        {
            int? fy = dict.TryGetValue("fy", out var obj) ? (int)obj : null;

            if (fy is < 0 or > 99 or null)
                context.Result = new BadRequestObjectResult("Fiscal year must be between 0 and 99");
        }

        if (dict.ContainsKey("name"))
        {
            var org = dict.TryGetValue("name", out var obj) ? (string)obj : null;

            if (string.IsNullOrWhiteSpace(org))
                context.Result = new BadRequestObjectResult("Organization name cannot be empty");
        }

        if (dict.ContainsKey("id"))
        {
            int? id = dict.TryGetValue("id", out var obj) ? (int)obj : null;

            if (id is < 1 or null) context.Result = new BadRequestObjectResult("ID must be greater than 0");
        }
    }
}