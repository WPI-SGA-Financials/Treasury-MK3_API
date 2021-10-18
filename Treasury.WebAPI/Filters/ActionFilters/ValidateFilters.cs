using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Treasury.Application.Contracts.V1.Requests;

namespace Treasury.WebAPI.Filters.ActionFilters
{
    public class ValidateFilters : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("request", out var temp);

            GeneralPagedRequest request = (GeneralPagedRequest)temp;

            if (!PaginationValid(request))
            {
                context.Result = new BadRequestObjectResult("Page and Results per a page (Rpp) must be greater than zero");
                return;
            }

            request = CleanInput(request);

            context.ActionArguments["request"] = request;
        }

        private static bool PaginationValid(GeneralPagedRequest request)
        {
            return request.Page > 0 && request.Rpp > 0;
        }
        
        private static GeneralPagedRequest CleanInput(GeneralPagedRequest request)
        {
            if (request.Name.Length > 0)
            {
                request.Name = request.Name.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            }
            
            if (request.Acronym.Length > 0)
            {
                request.Acronym = request.Acronym.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            }
            
            if (request.Classification.Length > 0)
            {
                request.Classification = request.Classification.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            }
            
            if (request.Type.Length > 0)
            {
                request.Type = request.Type.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            }
            
            return request;
        }
        
    }
}