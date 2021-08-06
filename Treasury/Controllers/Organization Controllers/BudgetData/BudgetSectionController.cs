using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treasury.Data;
using Treasury.Models.Financial_Models.Budget_Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Treasury.Controllers.Organization_Controllers.BudgetData
{
    [ApiController]
    public class BudgetSectionController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public BudgetSectionController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("api/organization/{name}/budgetsection")]
        public IEnumerable<BudgetSection> Get(string name)
        {
            return _dbContext.OrgBudgetSections.Where(b => b.NameOfClub.Equals(name));
        }

        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("api/organization/{name}/budgetsection/{fy}")]
        public IEnumerable<BudgetSection> Get(string name, int fy)
        {
            return _dbContext.OrgBudgetSections.Where(b => b.NameOfClub.Equals(name) && b.FiscalYear.Contains(""+fy));
        }

    }
}
