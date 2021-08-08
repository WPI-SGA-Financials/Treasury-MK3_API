using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treasury.Data;
using Treasury.Models.Financial_Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Treasury.Controllers.Financial_Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class FundingRequestController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public FundingRequestController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the Funding Requests
        /// </summary>
        /// <returns>List of Funding Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Financial Data" })]
        [Route("api/financials/fr")]
        public IEnumerable<FundingRequest> Get(string name)
        {
            return _dbContext.OrgFundingRequests;
        }

        /// <summary>
        /// Gets the Funding Requests for the given fiscal year
        /// </summary>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Funding Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Financial Data" })]
        [Route("api/financials/fr/{fy}")]
        public IEnumerable<FundingRequest> Get(string name, int fy)
        {
            return _dbContext.OrgFundingRequests.Where(b => b.FiscalYear.Contains("" + fy));
        }
    }
}
