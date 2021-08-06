using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using Treasury.Data;
using Treasury.Models;

namespace Treasury.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class OrganizationsController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public OrganizationsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all organizations from the database
        /// </summary>
        /// <returns>List of Organizations</returns>
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [HttpGet]
        public IEnumerable<Organization> Get()
        {
            return _dbContext.Organizations;
        }

        /// <summary>
        /// Gets a specific organization
        /// </summary>
        /// <param name="name">Name of Club</param>
        /// <returns>Basic details for the club</returns>
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [HttpGet("{name}")]
        public Organization Get(string name)
        {
            return _dbContext.Organizations.Find(name);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
