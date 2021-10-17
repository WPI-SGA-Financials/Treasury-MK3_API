using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.Accessor
{
    public class OrganizationAccessor
    {
        private readonly sgadbContext _dbContext;
        
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

        public List<OrganizationDto> GetFilteredOrganizations(string name)
        {
            List<OrganizationDto> orgs =  _dbContext.Organizations
                .Where(org => org.NameOfClub.Contains(name.Trim()))
                .Select(org => OrganizationDto.CreateDtoFromOrg(org))
                .ToList();

            return orgs.Any() ? orgs : null;
        }

        // Organization Data
        public OrganizationDetailDto GetOrganization(string name)
        {
            Organization org = _dbContext.Organizations
                .Include(org => org.ClubClassification)
                .Include(org => org.TechsyncName)
                .FirstOrDefault(org => org.NameOfClub.Equals(name.Trim()));

            return org != null ? OrganizationDetailDto.CreateDtoFromOrg(org) : null;
        }
    }
}