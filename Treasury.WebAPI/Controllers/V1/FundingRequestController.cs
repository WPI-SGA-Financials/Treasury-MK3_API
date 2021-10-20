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
using Treasury.Application.Errors;
using Treasury.WebAPI.Filters.ActionFilters;

namespace Treasury.WebAPI.Controllers.V1
{
    [Produces("application/json")]
    [ApiController]
    public class FundingRequestController : ControllerBase
    {
        private readonly FundingRequestAccessor _accessor;

        public FundingRequestController(FundingRequestAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Get Funding Requests based on optional filters in a paged response
        /// </summary>
        /// <returns>List of Funding Requests</returns>
        [HttpPost(ApiRoutes.FundingRequest.GetAll)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.FundingRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<FundingRequestDto>))]
        [ValidatePaginationAndFilters]
        public IActionResult Get([FromBody] FinancialPagedRequest request)
        {
            List<FundingRequestDto> dto = _accessor.GetFundingRequests(request, out var maxResults);

            PagedResponse<FundingRequestDto> response = new(dto)
            {
                PageNumber = request.Page,
                ResultsPerPage = request.Rpp,
                MaxResults = maxResults
            };

            response.Message = $"Successfully received {response.Data.Count()} Funding Requests.";

            return Ok(response);
        }

        /// <summary>
        /// Gets a specific funding request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.FundingRequest.Get)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.FundingRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<FundingRequestDetailedDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [ValidateInputActionFilter]
        public IActionResult GetExtended(int id)
        {
            FundingRequestDetailedDto dto = _accessor.GetFundingRequestById(id);

            Response<FundingRequestDetailedDto> response = new(dto)
            {
                Message = dto != null
                    ? "Successfully received the requested Funding Request"
                    : "Funding Request was not found"
            };

            return dto == null ? NotFound(response) : Ok(response);
        }

        /// <summary>
        /// Gets the Funding Requests for the requested organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Funding Requests</returns>
        [HttpGet(ApiRoutes.FundingRequest.GetByOrg)]
        [SwaggerOperation(Tags =
            new[] { SwaggerTags.Campus, SwaggerTags.OrganizationData, SwaggerTags.FundingRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<FundingRequestDto>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [ValidateInputActionFilter]
        public IActionResult Get(string name)
        {
            List<FundingRequestDto> dto = _accessor.GetFundingRequestsByOrganization(name);

            Response<List<FundingRequestDto>> response = new(dto)
            {
                Message = dto != null
                    ? "Successfully received the requested Organization's Funding Requests"
                    : "The Organization's Funding Requests were not found"
            };

            return dto == null ? NotFound(response) : Ok(response);
        }
    }
}