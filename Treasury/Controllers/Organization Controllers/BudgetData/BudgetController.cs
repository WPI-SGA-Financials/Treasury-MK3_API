using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treasury.Data;
using Treasury.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Treasury.Controllers.Organization_Controllers
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

        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("api/organization/{name}/budget")]
        public IEnumerable<Budget> Get(string name)
        {
            return _dbContext.OrgBudgets.Where(b => b.NameOfClub.Equals(name));
        }

        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("api/organization/{name}/budget/{fy}")]
        public IEnumerable<Budget> Get(string name, int fy)
        {
            return _dbContext.OrgBudgets.Where(b => b.NameOfClub.Equals(name) && b.FiscalYear.Contains("" + fy));
        }

    }
}
