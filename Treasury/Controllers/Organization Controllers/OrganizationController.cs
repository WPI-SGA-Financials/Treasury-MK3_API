using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treasury.Data;
using Treasury.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Treasury.Controllers.Organization_Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public OrganizationController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets a specific organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>Basic details for the club</returns>
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [HttpGet("{name}")]
        public Organization Get(string name)
        {
            return _dbContext.Organizations.Find(name);
        }
    }
}
