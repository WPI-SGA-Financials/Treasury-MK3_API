using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Treasury.Application.Accessor;
using Treasury.Application.DTOs;
using Treasury.Application.Errors;
using Treasury.Contracts.V1;
using Treasury.WebAPI.Filters.ActionFilters;

namespace Treasury.WebAPI.Controllers.V1
{
    [Produces("application/json")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly OrganizationAccessor _accessor;

        public OrganizationsController(OrganizationAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Gets all organizations
        /// </summary>
        /// <returns>List of Organizations</returns>
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.OrganizationData })]
        [HttpGet(ApiRoutes.Organizations.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrganizationDto>))]
        public List<OrganizationDto> Get()
        {
            return _accessor.GetOrganizations();
        }

        /// <summary>
        /// Gets organizations matching search
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Organizations</returns>
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.OrganizationData })]
        [HttpGet(ApiRoutes.Organizations.FilterByName)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrganizationDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult Get(string name)
        {
            List<OrganizationDto> dto =  _accessor.GetFilteredOrganizations(name);

            return dto == null ? NotFound() : Ok(dto);
        }
        
        /// <summary>
        /// Gets a specific organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>Basic details for the club</returns>
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.OrganizationData })]
        [HttpGet(ApiRoutes.Organization.Get)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationDetailDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult GetOrg(string name)
        {
            OrganizationDetailDto dto = _accessor.GetOrganization(name);

            return dto == null ? NotFound() : Ok(dto);
        }
    }
}
