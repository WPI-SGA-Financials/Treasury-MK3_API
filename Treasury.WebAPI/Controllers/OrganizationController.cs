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
    public class OrganizationsController : ControllerBase
    {
        private sgadbContext _dbContext;

        public OrganizationsController(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all organizations
        /// </summary>
        /// <returns>List of Organizations</returns>
        [SwaggerOperation(Tags = new[] { "Organizations Data" })]
        [HttpGet("organizations")]
        public List<OrganizationDto> Get()
        {
            return new OrganizationAccessor(_dbContext).GetOrganizations();
        }

        /// <summary>
        /// Gets organizations matching search
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Organizations</returns>
        [SwaggerOperation(Tags = new[] { "Organizations Data" })]
        [HttpGet("organizations/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrganizationDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidArgumentsError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public ActionResult<List<OrganizationDto>> Get(string name)
        {
            var res =  new OrganizationAccessor(_dbContext).GetFilteredOrganizations(name);
            
            return res switch
            {
                InvalidArgumentsError invalidArguments => BadRequest(invalidArguments),
                NotFoundError notFoundError => NotFound(notFoundError),
                _ => Ok(res)
            };
        }
        
        /// <summary>
        /// Gets a specific organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>Basic details for the club</returns>
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [HttpGet("organization/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidArgumentsError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public ActionResult<OrganizationDto> GetOrg(string name)
        {
            var res = new OrganizationAccessor(_dbContext).GetOrganization(name);
            
            return res switch
            {
                InvalidArgumentsError invalidArguments => BadRequest(invalidArguments),
                NotFoundError notFoundError => NotFound(notFoundError),
                _ => Ok(res)
            };
        }
    }
}
