using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treasury.Data;
using Treasury.Models;
using Treasury.Models.Financial_Models.Budget_Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Treasury.Controllers.Financial_Controllers.Budget_Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public BudgetController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all Budgets
        /// </summary>
        /// <returns>List of Budgets</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] {"Financial Data"})]
        [Route("api/financials/budgets")]
        public IEnumerable<Budget> Get()
        {
            return _dbContext.OrgBudgets;
        }

        /// <summary>
        /// Gets all Budgets for the given fiscal year
        /// </summary>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Budgets</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] {"Financial Data"})]
        [Route("api/financials/budgets/{fy}")]
        public IEnumerable<Budget> Get(int fy)
        {
            return _dbContext.OrgBudgets.Where(b => b.FiscalYear.Contains("" + fy));
        }

        /// <summary>
        /// Gets a single budget based on the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] {"Financial Data"})]
        [Route("api/financials/budget/{id}")]
        public ExtendedBudget GetExtended(int id)
        {
            Budget budget = _dbContext.OrgBudgets.Find(id);
            ExtendedBudget extendedBudget;

            if (budget is not null)
            {
                extendedBudget = ExtendedBudget.createFromBudget(budget);
                if (budget.NumOfItems > 0)
                {
                    extendedBudget.BudgetSections =
                        _dbContext.OrgBudgetSections.Where(bs => bs.BudgetID == id).ToList();
                }

                return extendedBudget;
            }

            return null;
        }
    }
}