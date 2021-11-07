using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Treasury.Application.Accessor;
using Treasury.Application.Contracts.V1;
using Treasury.Application.Contracts.V1.Requests;
using Treasury.Application.Contracts.V1.Responses;
using Treasury.Application.DTOs;
using Treasury.WebAPI.Filters.ActionFilters;

namespace Treasury.WebAPI.Controllers.V1
{
    [Produces("application/json")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly BudgetAccessor _accessor;

        public BudgetController(BudgetAccessor budgetAccessor)
        {
            _accessor = budgetAccessor;
        }

        /// <summary>
        /// Get Budgets based on optional filters in a paged response
        /// </summary>
        /// <returns>List of Budgets</returns>
        [HttpPost(ApiRoutes.Budget.GetAll)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.Budgets })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<BudgetDto>))]
        [ValidatePaginationAndFilters]
        public IActionResult Get([FromBody] FinancialPagedRequest request)
        {
            List<BudgetDto> dto = _accessor.GetBudgets(request, out var maxResults);

            PagedResponse<BudgetDto> response = new(dto)
            {
                PageNumber = request.Page,
                ResultsPerPage = request.Rpp,
                MaxResults = maxResults
            };

            response.Message = $"Successfully received {response.Data.Count()} Budgets.";

            return Ok(response);
        }

        /// <summary>
        /// Gets a single budget based on the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Budget.Get)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.Budgets })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<BudgetDetailedDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [ValidateInputActionFilter]
        public IActionResult GetExtended(int id)
        {
            BudgetDetailedDto dto = _accessor.GetBudgetById(id);

            Response<BudgetDetailedDto> response = new(dto)
            {
                Message = dto != null ? "Successfully received the requested Budget" : "Budget was not found"
            };

            return dto == null ? NotFound(response) : Ok(response);
        }

        /// <summary>
        /// Gets the budgets for a given organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Budgets</returns>
        [HttpGet(ApiRoutes.Budget.GetByOrg)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.OrganizationData, SwaggerTags.Budgets })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<BudgetDto>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [ValidateInputActionFilter]
        public IActionResult Get(string name)
        {
            List<BudgetDto> dto = _accessor.GetBudgetByOrganization(name);

            Response<List<BudgetDto>> response = new(dto)
            {
                Message = dto != null
                    ? "Successfully received the requested Organization's Budgets"
                    : "The Organization's Budgets were not found"
            };

            return dto == null ? NotFound(response) : Ok(response);
        }
    }
}