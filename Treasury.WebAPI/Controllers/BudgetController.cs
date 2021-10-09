using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Treasury.Application.Accessor;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;

namespace Treasury.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private sgadbContext _dbContext;

        public BudgetController(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        /// <summary>
        /// Gets all Budgets
        /// </summary>
        /// <returns>List of Budgets</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] {"Financial Data"})]
        [Route("financials/budgets")]
        public List<BudgetDto> Get()
        {
            return new BudgetAccessor(_dbContext).GetBudgets();
        }

        /// <summary>
        /// Gets all Budgets for the given fiscal year
        /// </summary>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Budgets</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] {"Financial Data"})]
        [Route("financials/budgets/{fy:int}")]
        public List<BudgetDto> Get(int fy)
        {
            return new BudgetAccessor(_dbContext).GetBudgetsByFy(fy);
        }

        /// <summary>
        /// Gets a single budget based on the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] {"Financial Data"})]
        [Route("financials/budget/{id:int}")]
        public BudgetDetailedDto GetExtended(int id)
        {
            return new BudgetAccessor(_dbContext).GetBudgetById(id);
        }

        /// <summary>
        /// Gets the budgets for a given organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Budgets</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("organization/{name}/budget")]
        public List<BudgetDto> Get(string name)
        {
            return new BudgetAccessor(_dbContext).GetBudgetByOrganization(name);
        }

        /// <summary>
        /// Gets the budget for an organization in a given fiscal year
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>Budget for that Year</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("organization/{name}/budget/{fy:int}")]
        public List<BudgetDto> Get(string name, int fy)
        {
            return new BudgetAccessor(_dbContext).GetBudgetByOrganizationFy(name, fy);
        }

    }
}