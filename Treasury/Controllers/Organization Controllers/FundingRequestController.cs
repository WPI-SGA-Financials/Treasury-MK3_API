using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using Treasury.Data;
using Treasury.Models.Financial_Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Treasury.Controllers.Organization_Controllers
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
        /// Gets the Funding Requests for the requested organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Funding Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("api/organization/{name}/fr")]
        public IEnumerable<FundingRequest> Get(string name)
        {
            return _dbContext.OrgFundingRequests.Where(b => b.NameOfClub.Equals(name));
        }

        /// <summary>
        /// Gets the Funding Requests for the requested organization and fiscal year
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Funding Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("api/organization/{name}/fr/{fy}")]
        public IEnumerable<FundingRequest> Get(string name, int fy)
        {
            return _dbContext.OrgFundingRequests.Where(b => b.NameOfClub.Equals(name) && b.FiscalYear.Contains("" + fy));
        }

    }
}
