using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Treasury.Application.Accessor;
using Treasury.Application.Contracts.V1;
using Treasury.Application.Contracts.V1.Requests;
using Treasury.Application.Contracts.V1.Responses;
using Treasury.Application.DTOs;
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
        /// Get Reallocation Requests based on optional filters in a paged response
        /// </summary>
        /// <returns>List of Reallocation Requests</returns>
        [HttpPost(ApiRoutes.ReallocationRequest.GetAll)]
        [SwaggerOperation(Tags = new[]
            { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.ReallocationRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<ReallocationRequestDto>))]
        public IActionResult Get([FromBody] FinancialPagedRequest request)
        {
            List<ReallocationRequestDto> dto = _accessor.GetReallocationRequests(request, out var maxResults);

            PagedResponse<ReallocationRequestDto> response = new(dto)
            {
                PageNumber = request.Page,
                ResultsPerPage = request.Rpp,
                MaxResults = maxResults
            };

            response.Message = $"Successfully received {response.Data.Count()} Reallocation Requests.";

            return Ok(response);
        }

        /// <summary>
        /// Gets the Reallocation Request by ID
        /// </summary>
        /// <param name="id">Reallocation ID</param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.ReallocationRequest.Get)]
        [SwaggerOperation(Tags = new[]
            { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.ReallocationRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ReallocationRequestDetailedDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [ValidateInputActionFilter]
        public IActionResult GetById(int id)
        {
            ReallocationRequestDetailedDto dto = _accessor.GetReallocationRequestById(id);

            Response<ReallocationRequestDetailedDto> response = new(dto)
            {
                Message = dto != null
                    ? "Successfully received the requested Reallocation Request"
                    : "Reallocation Request was not found"
            };

            return dto == null ? NotFound(response) : Ok(response);
        }

        /// <summary>
        /// Gets the Reallocation Requests for the requested organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Reallocation Requests</returns>
        [HttpGet(ApiRoutes.ReallocationRequest.GetByOrg)]
        [SwaggerOperation(Tags = new[]
            { SwaggerTags.Campus, SwaggerTags.OrganizationData, SwaggerTags.ReallocationRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<ReallocationRequestDto>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [ValidateInputActionFilter]
        public IActionResult Get(string name)
        {
            List<ReallocationRequestDto> dto = _accessor.GetReallocationRequestsByOrganization(name);

            Response<List<ReallocationRequestDto>> response = new(dto)
            {
                Message = dto != null
                    ? "Successfully received the requested Organization's Reallocation Requests"
                    : "The Organization's Reallocation Requests were not found"
            };

            return dto == null ? NotFound(response) : Ok(response);
        }
    }
}