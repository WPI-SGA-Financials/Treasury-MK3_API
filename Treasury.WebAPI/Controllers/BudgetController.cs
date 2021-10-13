using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Treasury.Application.Accessor;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Application.Errors;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BudgetDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidArgumentsError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public ActionResult<List<BudgetDto>> Get(int fy)
        {
            var res = new BudgetAccessor(_dbContext).GetBudgetsByFy(fy);
            
            return res switch
            {
                InvalidArgumentsError invalidArguments => BadRequest(invalidArguments),
                NotFoundError notFoundError => NotFound(notFoundError),
                _ => Ok(res)
            };
        }

        /// <summary>
        /// Gets a single budget based on the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] {"Financial Data"})]
        [Route("financials/budget/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BudgetDetailedDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidArgumentsError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public ActionResult<BudgetDetailedDto> GetExtended(int id)
        {
            var res =  new BudgetAccessor(_dbContext).GetBudgetById(id);
            
            return res switch
            {
                InvalidArgumentsError invalidArguments => BadRequest(invalidArguments),
                NotFoundError notFoundError => NotFound(notFoundError),
                _ => Ok(res)
            };
        }

        /// <summary>
        /// Gets the budgets for a given organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Budgets</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("organization/{name}/budgets")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BudgetDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidArgumentsError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public ActionResult<List<BudgetDto>> Get(string name)
        {
            var res = new BudgetAccessor(_dbContext).GetBudgetByOrganization(name);
            
            return res switch
            {
                InvalidArgumentsError invalidArguments => BadRequest(invalidArguments),
                NotFoundError notFoundError => NotFound(notFoundError),
                _ => Ok(res)
            };
        }

        /// <summary>
        /// Gets the budget for an organization in a given fiscal year
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>Budget for that Year</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("organization/{name}/budgets/{fy:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BudgetDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidArgumentsError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public ActionResult<BudgetDto> Get(string name, int fy)
        {
            var res = new BudgetAccessor(_dbContext).GetBudgetByOrganizationFy(name, fy);

            return res switch
            {
                InvalidArgumentsError invalidArguments => BadRequest(invalidArguments),
                NotFoundError notFoundError => NotFound(notFoundError),
                _ => Ok(res)
            };
        }

    }
}