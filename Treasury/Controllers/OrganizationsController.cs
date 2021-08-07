using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
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
        [SwaggerOperation(Tags = new[] { "Organizations Data" })]
        [HttpGet]
        public IEnumerable<Organization> Get()
        {
            return _dbContext.Organizations;
        }

        /// <summary>
        /// Gets organizations matching search
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Organizations</returns>
        [SwaggerOperation(Tags = new[] { "Organizations Data" })]
        [HttpGet("{name}")]
        public IEnumerable<Organization> Get(string name)
        {
            return _dbContext.Organizations.Where((o => o.Name.Contains(name)));
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
