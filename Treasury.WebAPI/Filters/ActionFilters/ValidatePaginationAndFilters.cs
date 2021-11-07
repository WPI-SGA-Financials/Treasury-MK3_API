using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Treasury.Application.Contracts.V1.Requests;
using Treasury.WebAPI.Util;

namespace Treasury.WebAPI.Filters.ActionFilters
{
    public class ValidatePaginationAndFilters : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("request", out var temp);

            if (!PaginationValid(temp as IPagedRequest))
            {
                context.Result = new BadRequestObjectResult("Page and Results per a page (Rpp) must be greater than zero");
                return;
            }

            if (temp is FinancialPagedRequest finRequest)
            {
                finRequest = CleanFinancialInput(finRequest);

                context.ActionArguments["request"] = finRequest;
            }
            else
            {
                GeneralPagedRequest request = (GeneralPagedRequest)temp;

                request = CleanGeneralInput(request);

                context.ActionArguments["request"] = request;
            }
        }

        private static bool PaginationValid(IPagedRequest request)
        {
            return request.Page > 0 && request.Rpp > 0;
        }
        
        private static GeneralPagedRequest CleanGeneralInput(GeneralPagedRequest request)
        {
            request.Name = HelperFunctions.CleanName(request.Name);
            request.Acronym = HelperFunctions.CleanAcronym(request.Acronym);
            request.Classification = HelperFunctions.CleanClassification(request.Classification);
            request.Type = HelperFunctions.CleanType(request.Type);

            return request;
        }
        
        private static FinancialPagedRequest CleanFinancialInput(FinancialPagedRequest request)
        {
            request.Name = HelperFunctions.CleanName(request.Name);
            request.Acronym = HelperFunctions.CleanAcronym(request.Acronym);
            request.Classification = HelperFunctions.CleanClassification(request.Classification);
            request.Type = HelperFunctions.CleanType(request.Type);
            
            return request;
        }

    }
}