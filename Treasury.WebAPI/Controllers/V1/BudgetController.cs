using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Treasury.Application.Accessor;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Application.Errors;
using Treasury.Contracts.V1;
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
        /// Gets all Budgets
        /// </summary>
        /// <returns>List of Budgets</returns>
        [HttpGet(ApiRoutes.Budget.GetAll)]        
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.Budgets })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BudgetDto>))]
        public List<BudgetDto> Get()
        {
            return _accessor.GetBudgets();
        }

        /// <summary>
        /// Gets all Budgets for the given fiscal year
        /// </summary>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Budgets</returns>
        [HttpGet(ApiRoutes.Budget.GetByFy)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.Budgets })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BudgetDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult Get(int fy)
        {
            List<BudgetDto> dto = _accessor.GetBudgetsByFy(fy);

            return dto == null ? NotFound() : Ok(dto);
        }

        /// <summary>
        /// Gets a single budget based on the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Budget.Get)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.Budgets })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BudgetDetailedDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult GetExtended(int id)
        {
            BudgetDetailedDto dto = _accessor.GetBudgetById(id);

            return dto == null ? NotFound() : Ok(dto);
        }

        /// <summary>
        /// Gets the budgets for a given organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Budgets</returns>
        [HttpGet(ApiRoutes.Budget.GetByOrg)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.OrganizationData, SwaggerTags.Budgets })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BudgetDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult Get(string name)
        {
            List<BudgetDto> dto = _accessor.GetBudgetByOrganization(name);

            return dto == null ? NotFound() : Ok(dto);
        }

        /// <summary>
        /// Gets the budget for an organization in a given fiscal year
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>Budget for that Year</returns>
        [HttpGet(ApiRoutes.Budget.GetByOrgFy)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.OrganizationData, SwaggerTags.Budgets })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BudgetDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult Get(string name, int fy)
        {
            BudgetDto dto = _accessor.GetBudgetByOrganizationFy(name, fy);

            return dto == null ? NotFound() : Ok(dto);
        }
    }
}