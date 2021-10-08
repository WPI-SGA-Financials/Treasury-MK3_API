using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Treasury.Application.Accessor;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;

namespace Treasury.WebAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    [Produces("application/json")]
    public class ReallocationController : ControllerBase
    {
        private sgadbContext _dbContext;

        public ReallocationController(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        /// <summary>
        /// Gets the Reallocation Requests
        /// </summary>
        /// <returns>List of Reallocation Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Financial Data" })]
        [Route("financials/reallocs")]
        public List<ReallocationRequestDto> Get()
        {
            return new ReallocationRequestAccessor(_dbContext).GetReallocationRequests();
        }

        /// <summary>
        /// Gets the Reallocation Requests for the requested fiscal year
        /// </summary>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Reallocation Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Financial Data" })]
        [Route("financials/reallocs/{fy:int}")]
        public List<ReallocationRequestDto> Get(int fy)
        {
            return new ReallocationRequestAccessor(_dbContext).GetReallocationRequestsByFy(fy);
        }
        
        /// <summary>
        /// Gets the Reallocation Request by ID
        /// </summary>
        /// <param name="id">Reallocation ID</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags= new [] {"Financial Data"})]
        [Route("financials/realloc/{id:int}")]
        public ReallocationRequestDetailedDto GetById(int id)
        {
            return new ReallocationRequestAccessor(_dbContext).GetReallocationRequestById(id);
        }

        /// <summary>
        /// Gets the Reallocation Requests for the requested organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Reallocation Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("organization/{name}/reallocs")]
        public List<ReallocationRequestDto> Get(string name)
        {
            return new ReallocationRequestAccessor(_dbContext).GetReallocationRequestsByOrganization(name);
        }

        /// <summary>
        /// Gets the Reallocation Requests for the requested organization and fiscal year
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Reallocation Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("organization/{name}/reallocs/{fy:int}")]
        public List<ReallocationRequestDto> Get(string name, int fy)
        {
            return new ReallocationRequestAccessor(_dbContext).GetReallocationRequestsByOrganizationFy(name, fy);
        }
    }
}