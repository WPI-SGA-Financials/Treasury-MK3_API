using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using Treasury.Application.Accessor;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;

namespace Treasury.WebAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    
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
        /// Gets all organizations from the database
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
        public List<OrganizationDto> Get(string name)
        {
            return new OrganizationAccessor(_dbContext).GetFilteredOrganizations(name);
        }
        
        /// <summary>
        /// Gets a specific organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>Basic details for the club</returns>
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [HttpGet("organization/{name}")]
        public OrganizationDetailDto GetOrg(string name)
        {
            return new OrganizationAccessor(_dbContext).GetOrganization(name);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
