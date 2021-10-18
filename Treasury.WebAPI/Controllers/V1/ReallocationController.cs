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
    [ApiController]
    [Produces("application/json")]
    public class ReallocationController : ControllerBase
    {
        private readonly ReallocationRequestAccessor _accessor;

        public ReallocationController(ReallocationRequestAccessor accessor)
        {
            _accessor = accessor;
        }
        
        /// <summary>
        /// Gets the Reallocation Requests
        /// </summary>
        /// <returns>List of Reallocation Requests</returns>
        [HttpGet(ApiRoutes.ReallocationRequest.GetAll)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.ReallocationRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReallocationRequestDto>))]
        public List<ReallocationRequestDto> Get()
        {
            return _accessor.GetReallocationRequests();
        }

        /// <summary>
        /// Gets the Reallocation Requests for the requested fiscal year
        /// </summary>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Reallocation Requests</returns>
        [HttpGet(ApiRoutes.ReallocationRequest.GetByFy)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.ReallocationRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReallocationRequestDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult Get(int fy)
        {
            List<ReallocationRequestDto> dto = _accessor.GetReallocationRequestsByFy(fy);
            
            return dto == null ? NotFound() : Ok(dto);
        }
        
        /// <summary>
        /// Gets the Reallocation Request by ID
        /// </summary>
        /// <param name="id">Reallocation ID</param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.ReallocationRequest.Get)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.ReallocationRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReallocationRequestDetailedDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult GetById(int id)
        {
            ReallocationRequestDetailedDto dto = _accessor.GetReallocationRequestById(id);
            
            return dto == null ? NotFound() : Ok(dto);
        }

        /// <summary>
        /// Gets the Reallocation Requests for the requested organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Reallocation Requests</returns>
        [HttpGet(ApiRoutes.ReallocationRequest.GetByOrg)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.OrganizationData, SwaggerTags.ReallocationRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReallocationRequestDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult Get(string name)
        {
            List<ReallocationRequestDto> dto = _accessor.GetReallocationRequestsByOrganization(name);
            
            return dto == null ? NotFound() : Ok(dto);
        }

        /// <summary>
        /// Gets the Reallocation Requests for the requested organization and fiscal year
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Reallocation Requests</returns>
        [HttpGet(ApiRoutes.ReallocationRequest.GetByOrgFy)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.OrganizationData, SwaggerTags.ReallocationRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReallocationRequestDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult Get(string name, int fy)
        {
            List<ReallocationRequestDto> dto = _accessor.GetReallocationRequestsByOrganizationFy(name, fy);

            return dto == null ? NotFound() : Ok(dto);
        }
    }
}