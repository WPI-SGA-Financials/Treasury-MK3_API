using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treasury.Data;
using Treasury.Models.Financial_Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Treasury.Controllers.Financial_Controllers
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
        /// Gets the Reallocation Requests
        /// </summary>
        /// <returns>List of Reallocation Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Financial Data" })]
        [Route("api/financials/reallocs")]
        public IEnumerable<Reallocation> Get()
        {
            return _dbContext.OrgReallocations;
        }

        /// <summary>
        /// Gets the Reallocation Requests for the requested fiscal year
        /// </summary>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Reallocation Requests</returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Financial Data" })]
        [Route("api/financials/reallocs/{fy}")]
        public IEnumerable<Reallocation> Get(int fy)
        {
            return _dbContext.OrgReallocations.Where(b => b.FiscalYear.Contains("" + fy));
        }

        /// <summary>
        /// Gets the Reallocation Request by ID
        /// </summary>
        /// <param name="id">Reallocation ID</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags= new [] {"Financial Data"})]
        [Route("api/financials/realloc/{id}")]
        public Reallocation GetByID(int id)
        {
            // TODO: Come back to when meetings table is created
            return _dbContext.OrgReallocations.Find(id);
        }
    }
}
