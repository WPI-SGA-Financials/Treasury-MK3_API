using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treasury.Data;
using Treasury.Models.Financial_Models;
using Treasury.Models.Financial_Models.Funding_Request_Models;

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
        [Route("api/financials/frs")]
        public IEnumerable<FundingRequest> Get()
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
        [Route("api/financials/frs/{fy}")]
        public IEnumerable<FundingRequest> Get(int fy)
        {
            return _dbContext.OrgFundingRequests.Where(b => b.FiscalYear.Contains("" + fy));
        }

        /// <summary>
        /// Gets a specific funding request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] {"Financial Data"})]
        [Route("api/financials/fr/{id}")]
        public ExtendedFundingRequest GetExtended(int id)
        {
            FundingRequest fr = _dbContext.OrgFundingRequests.Find(id);
            
            // TODO: Come back to when meetings table is created
            return ExtendedFundingRequest.createFromFR(fr);
        }
    }
}
