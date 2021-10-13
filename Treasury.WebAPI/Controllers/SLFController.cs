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
    [Route("api/financials/[controller]")]
    [ApiController]
    public class SLFController : ControllerBase
    {
        private sgadbContext _dbContext;

        public SLFController(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all Student Life Fee Data
        /// </summary>
        /// <returns>List of Student Life Fees</returns>
        [SwaggerOperation(Tags = new [] {"Financial Data"})]
        [HttpGet]
        public IEnumerable<StudentLifeFeeDto> Get()
        {
            return new SlfAccessor(_dbContext).GetSlfs();
        }

        /// <summary>
        /// Gets Student Life Fee data for the given fiscal year
        /// </summary>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>Student Life Fee data for the year</returns>
        [SwaggerOperation(Tags = new [] {"Financial Data"})]
        [HttpGet("{fy:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentLifeFeeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidArgumentsError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        public ActionResult<StudentLifeFeeDto> Get(int fy)
        {
            var res = new SlfAccessor(_dbContext).GetSlfByFy(fy);
            
            return res switch
            {
                InvalidArgumentsError invalidArguments => BadRequest(invalidArguments),
                NotFoundError notFoundError => NotFound(notFoundError),
                _ => Ok(res)
            };
        }
    }
}