﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Treasury.Application.Accessor;
using Treasury.Application.Contracts.V1;
using Treasury.Application.Contracts.V1.Responses;
using Treasury.Application.DTOs;

namespace Treasury.WebAPI.Controllers.V1
{
    [Produces("application/json")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        private readonly MetadataAccessor _accessor;
        
        public MetadataController(MetadataAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Get all Metadata values
        /// </summary>
        /// <returns>All Metadata values</returns>
        [HttpGet(ApiRoutes.Metadata.All)]
        [SwaggerOperation(Tags = new []{SwaggerTags.Campus, SwaggerTags.Metadata})]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<AllMetadataDto>))]
        public IActionResult GetAll()
        {
            AllMetadataDto dto = new AllMetadataDto
            {
                ClubClassifications = _accessor.GetClassifications(),
                ClubTypes = _accessor.GetClubTypes()
            };

            return Ok(new Response<AllMetadataDto>(dto)
            {
                Message = "Successfully received all metadata values."
            });
        }
        
        /// <summary>
        /// Get the List of Club Classifications
        /// </summary>
        /// <returns>List of Club Classifications</returns>
        [HttpGet(ApiRoutes.Metadata.Classification)]
        [SwaggerOperation(Tags = new []{SwaggerTags.Campus, SwaggerTags.Metadata})]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<ClassificationDto>>))]
        public IActionResult GetClassifications()
        {
            List<ClassificationDto> dtos = _accessor.GetClassifications();

            Response<List<ClassificationDto>> response = new(dtos)
            {
                Message = "Successfully returned the list of Club Classifications."
            };

            return Ok(response);
        }
        
        /// <summary>
        /// Get the List of Club Types
        /// </summary>
        /// <returns>List of Club Types</returns>
        [HttpGet(ApiRoutes.Metadata.ClubTypes)]
        [SwaggerOperation(Tags = new []{SwaggerTags.Campus, SwaggerTags.Metadata})]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<ClubTypeDto>>))]
        public IActionResult GetClubTypes()
        {
            List<ClubTypeDto> dtos = _accessor.GetClubTypes();

            Response<List<ClubTypeDto>> response = new(dtos)
            {
                Message = "Successfully returned the list of Club Types."
            };

            return Ok(response);
        }
    }
}