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
    public class FundingRequestController : ControllerBase
    {
        private readonly FundingRequestAccessor _accessor;

        public FundingRequestController(FundingRequestAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Gets the Funding Requests
        /// </summary>
        /// <returns>List of Funding Requests</returns>
        [HttpGet(ApiRoutes.FundingRequest.GetAll)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.FundingRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FundingRequestDto>))]
        public List<FundingRequestDto> Get()
        {
            return _accessor.GetFundingRequests();
        }

        /// <summary>
        /// Gets the Funding Requests for the given fiscal year
        /// </summary>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Funding Requests</returns>
        [HttpGet(ApiRoutes.FundingRequest.GetByFy)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.FundingRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FundingRequestDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult Get(int fy)
        {
            List<FundingRequestDto> dto = _accessor.GetFundingRequestsByFy(fy);

            return dto == null ? NotFound() : Ok(dto);
        }

        /// <summary>
        /// Gets a specific funding request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.FundingRequest.Get)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.FinancialData, SwaggerTags.FundingRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FundingRequestDetailedDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult GetExtended(int id)
        {
            FundingRequestDetailedDto dto = _accessor.GetFundingRequestById(id);

            return dto == null ? NotFound() : Ok(dto);
        }

        /// <summary>
        /// Gets the Funding Requests for the requested organization
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <returns>List of Funding Requests</returns>
        [HttpGet(ApiRoutes.FundingRequest.GetByOrg)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.OrganizationData, SwaggerTags.FundingRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FundingRequestDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult Get(string name)
        {
            List<FundingRequestDto> dto = _accessor.GetFundingRequestsByOrganization(name);

            return dto == null ? NotFound() : Ok(dto);
        }

        /// <summary>
        /// Gets the Funding Requests for the requested organization and fiscal year
        /// </summary>
        /// <param name="name">Club Name</param>
        /// <param name="fy">Fiscal Year</param>
        /// <returns>List of Funding Requests</returns>
        [HttpGet(ApiRoutes.FundingRequest.GetByOrgFy)]
        [SwaggerOperation(Tags = new[] { SwaggerTags.Campus, SwaggerTags.OrganizationData, SwaggerTags.FundingRequests })]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FundingRequestDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundError))]
        [ValidateInputActionFilter]
        public IActionResult Get(string name, int fy)
        {
            List<FundingRequestDto> dto = _accessor.GetFundingRequestsByOrganizationFy(name, fy);

            return dto == null ? NotFound() : Ok();
        }
    }
}