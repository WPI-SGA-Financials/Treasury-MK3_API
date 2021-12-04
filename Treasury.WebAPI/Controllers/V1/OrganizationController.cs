using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Treasury.Application.Accessor.Interface;
using Treasury.Application.Contracts.V1;
using Treasury.Application.Contracts.V1.Requests;
using Treasury.Application.Contracts.V1.Responses;
using Treasury.Application.DTOs;
using Treasury.WebAPI.Filters.ActionFilters;

namespace Treasury.WebAPI.Controllers.V1
{
    [Produces("application/json")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationAccessor _accessor;

        public OrganizationsController(IOrganizationAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Get Organizations based on optional filters in a paged response
        /// </summary>
        /// <returns>List of Organizations</returns>
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.OrganizationData })]
        [HttpPost(ApiRoutes.Organizations.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<OrganizationDto>))]
        [ValidatePaginationAndFilters]
        public IActionResult Get([FromBody] GeneralPagedRequest request)
        {
            List<OrganizationDto> dto = _accessor.GetOrganizations(request, out var maxResults);

            PagedResponse<OrganizationDto> response = new(dto)
            {
                PageNumber = request.Page,
                ResultsPerPage = request.Rpp,
                MaxResults = maxResults
            };

            response.Message = $"Successfully received {response.Data.Count()} Organizations.";

            return Ok(response);
        }

        /// <summary>
        /// Gets a specific organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>Basic details for the club</returns>
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.OrganizationData })]
        [HttpGet(ApiRoutes.Organization.Get)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<OrganizationDetailDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [ValidateInputActionFilter]
        public IActionResult GetOrg(string name)
        {
            OrganizationDetailDto dto = _accessor.GetOrganization(name);

            Response<OrganizationDetailDto> response = new(dto)
            {
                Message = dto != null
                    ? "Successfully received the requested Organization"
                    : "Organization was not found"
            };

            return dto == null ? NotFound(response) : Ok(response);
        }
    }
}