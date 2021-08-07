using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treasury.Data;
using Treasury.Models.Financial_Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Treasury.Controllers.Organization_Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class ReallocationsController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public ReallocationsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the Reallocation Requests for the requested organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Reallocation Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("api/organization/{name}/realloc")]
        public IEnumerable<Reallocation> Get(string name)
        {
            return _dbContext.OrgReallocations.Where(b => b.NameOfClub.Equals(name));
        }

        /// <summary>
        /// Gets the Reallocation Requests for the requested organization and fiscal year
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Reallocation Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Organization Data" })]
        [Route("api/organization/{name}/realloc/{fy}")]
        public IEnumerable<Reallocation> Get(string name, int fy)
        {
            return _dbContext.OrgReallocations.Where(b => b.NameOfClub.Equals(name) && b.FiscalYear.Contains("" + fy));
        }
    }
}
