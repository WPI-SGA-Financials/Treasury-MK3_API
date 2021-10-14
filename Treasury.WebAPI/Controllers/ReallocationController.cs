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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReallocationRequestDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidArgumentsError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public ActionResult<List<ReallocationRequestDto>> Get(int fy)
        {
            var res = new ReallocationRequestAccessor(_dbContext).GetReallocationRequestsByFy(fy);
            
            return res switch
            {
                InvalidArgumentsError invalidArguments => BadRequest(invalidArguments),
                NotFoundError notFoundError => NotFound(notFoundError),
                _ => Ok(res)
            };
        }
        
        /// <summary>
        /// Gets the Reallocation Request by ID
        /// </summary>
        /// <param name="id">Reallocation ID</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags= new [] {"Financial Data"})]
        [Route("financials/realloc/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReallocationRequestDetailedDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidArgumentsError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public ActionResult<ReallocationRequestDetailedDto> GetById(int id)
        {
            var res = new ReallocationRequestAccessor(_dbContext).GetReallocationRequestById(id);
            
            return res switch
            {
                InvalidArgumentsError invalidArguments => BadRequest(invalidArguments),
                NotFoundError notFoundError => NotFound(notFoundError),
                _ => Ok(res)
            };
        }

        /// <summary>
        /// Gets the Reallocation Requests for the requested organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Reallocation Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("organization/{name}/reallocs")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReallocationRequestDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidArgumentsError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public ActionResult<List<ReallocationRequestDto>> Get(string name)
        {
            var res = new ReallocationRequestAccessor(_dbContext).GetReallocationRequestsByOrganization(name);
            
            return res switch
            {
                InvalidArgumentsError invalidArguments => BadRequest(invalidArguments),
                NotFoundError notFoundError => NotFound(notFoundError),
                _ => Ok(res)
            };
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReallocationRequestDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidArgumentsError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public ActionResult<List<ReallocationRequestDto>> Get(string name, int fy)
        {
            var res = new ReallocationRequestAccessor(_dbContext).GetReallocationRequestsByOrganizationFy(name, fy);
            
            return res switch
            {
                InvalidArgumentsError invalidArguments => BadRequest(invalidArguments),
                NotFoundError notFoundError => NotFound(notFoundError),
                _ => Ok(res)
            };
        }
    }
}