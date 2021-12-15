using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Treasury.Application.Accessor.Interface;
using Treasury.Application.Contracts.V1;
using Treasury.Application.Contracts.V1.Responses;
using Treasury.Application.DTOs;
using Treasury.WebAPI.Filters.ActionFilters;

namespace Treasury.WebAPI.Controllers.V1;

[Produces("application/json")]
[ApiController]
public class SlfController : ControllerBase
{
    private readonly IStudentLifeFeeAccessor _accessor;

    public SlfController(IStudentLifeFeeAccessor accessor)
    {
        _accessor = accessor;
    }

    /// <summary>
    ///     Gets all Student Life Fee Data
    /// </summary>
    /// <returns>List of Student Life Fees</returns>
    [HttpGet(ApiRoutes.StudentLifeFee.GetAll)]
    [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.StudentLifeFee })]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<StudentLifeFeeDto>>))]
    public IActionResult Get()
    {
        var dto = _accessor.GetSlfs();

        Response<List<StudentLifeFeeDto>> response = new(dto)
        {
            Message = "Successfully received all Student Life Fees"
        };

        return Ok(response);
    }

    /// <summary>
    ///     Gets Student Life Fee data for the given fiscal year
    /// </summary>
    /// <param name="fy">Fiscal Year</param>
    /// <returns>Student Life Fee data for the year</returns>
    [HttpGet(ApiRoutes.StudentLifeFee.GetByFy)]
    [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.StudentLifeFee })]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<StudentLifeFeeDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
    [ValidateInputActionFilter]
    public IActionResult Get(int fy)
    {
        var dto = _accessor.GetSlfByFy(fy);

        Response<StudentLifeFeeDto> response = new(dto)
        {
            Message = dto != null
                ? "Successfully received the requested Student Life Fee"
                : "The Student Life Fee was not found"
        };

        return dto == null ? NotFound(response) : Ok(response);
    }
}