using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Application.Errors;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.Accessor
{
    public class OrganizationAccessor
    {
        private sgadbContext _dbContext;

        public OrganizationAccessor(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // Organizations Data
        public List<OrganizationDto> GetOrganizations()
        {
            return _dbContext.Organizations
                .Select(org => OrganizationDto.CreateDtoFromOrg(org))
                .ToList();
        }

        public object GetFilteredOrganizations(string name)
        {
            if (ValidateModel(name) is { } validate)
            {
                return validate;
            }
            
            List<OrganizationDto> orgs =  _dbContext.Organizations
                .Where(org => org.NameOfClub.Contains(name.Trim()))
                .Select(org => OrganizationDto.CreateDtoFromOrg(org))
                .ToList();

            if (orgs.Any())
            {
                return orgs;
            }
            
            var errorDict = new Dictionary<string, object>
            {
                { "name", name }
            };

            return new NotFoundError("No organizations were found", errorDict);
        }

        // Organization Data
        public object GetOrganization(string name)
        {
            if (ValidateModel(name) is { } validate)
            {
                return validate;
            }
            
            Organization org = _dbContext.Organizations
                .Include(org => org.ClubClassification)
                .Include(org => org.TechsyncName)
                .FirstOrDefault(org => org.NameOfClub.Equals(name.Trim()));

            if (org != null)
            {
                return OrganizationDetailDto.CreateDtoFromOrg(org);
            }
            
            var errorDict = new Dictionary<string, object>
            {
                { "name", name }
            };

            return new NotFoundError("The requested organization was not found", errorDict);

        }
        
        private static InvalidArgumentsError ValidateModel(string organization)
        {
            Dictionary<string, object> errorDict = new Dictionary<string, object>();
            
            if (String.IsNullOrWhiteSpace(organization))
            {
                errorDict.Add("nameOfClub", "Organization name cannot be empty");
            }

            return errorDict.Count > 0 ? new InvalidArgumentsError("One or more parameters are invalid", errorDict) : null;
        }
    }
}