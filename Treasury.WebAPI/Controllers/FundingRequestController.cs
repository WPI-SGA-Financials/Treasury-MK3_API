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
    public class FundingRequestController : ControllerBase
    {
        private sgadbContext _dbContext;

        public FundingRequestController(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the Funding Requests
        /// </summary>
        /// <returns>List of Funding Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Financial Data" })]
        [Route("financials/frs")]
        public List<FundingRequestDto> Get()
        {
            return new FundingRequestAccessor(_dbContext).GetFundingRequests();
        }

        /// <summary>
        /// Gets the Funding Requests for the given fiscal year
        /// </summary>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Funding Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Financial Data" })]
        [Route("financials/frs/{fy}")]
        public List<FundingRequestDto> Get(int fy)
        {
            return new FundingRequestAccessor(_dbContext).GetFundingRequestsByFy(fy);
        }

        /// <summary>
        /// Gets a specific funding request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] {"Financial Data"})]
        [Route("financials/fr/{id:int}")]
        public FundingRequestDetailedDto GetExtended(int id)
        {
            return new FundingRequestAccessor(_dbContext).GetFundingRequestById(id);
        }

        /// <summary>
        /// Gets the Funding Requests for the requested organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Funding Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("organization/{name}/fr")]
        public List<FundingRequestDto> Get(string name)
        {
            return new FundingRequestAccessor(_dbContext).GetFundingRequestsByOrganization(name);
        }

        /// <summary>
        /// Gets the Funding Requests for the requested organization and fiscal year
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Funding Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("organization/{name}/fr/{fy:int}")]
        public List<FundingRequestDto> Get(string name, int fy)
        {
            return new FundingRequestAccessor(_dbContext).GetFundingRequestsByOrganizationFy(name, fy);
        }
    }
}