using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Treasury.Application.Accessor;
using Treasury.Application.Contracts.V1;
using Treasury.Application.DTOs;
using Treasury.Application.Errors;
using Treasury.WebAPI.Filters.ActionFilters;

namespace Treasury.WebAPI.Controllers.V1
{
    [Produces("application/json")]
    [ApiController]
    public class SlfController : ControllerBase
    {
        private readonly StudentLifeFeeAccessor _accessor;

        public SlfController(StudentLifeFeeAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Gets all Student Life Fee Data
        /// </summary>
        /// <returns>List of Student Life Fees</returns>
        [HttpGet(ApiRoutes.StudentLifeFee.GetAll)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.StudentLifeFee })]
        public IEnumerable<StudentLifeFeeDto> Get()
        {
            return _accessor.GetSlfs();
        }

        /// <summary>
        /// Gets Student Life Fee data for the given fiscal year
        /// </summary>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>Student Life Fee data for the year</returns>
        [HttpGet(ApiRoutes.StudentLifeFee.GetByFy)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.StudentLifeFee })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentLifeFeeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult Get(int fy)
        {
            StudentLifeFeeDto dto = _accessor.GetSlfByFy(fy);
            
            return dto == null ? NotFound() : Ok(dto);
        }
    }
}