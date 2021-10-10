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
        private sgadbContext _dbContext;

        public OrganizationAccessor(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // Organizations Data
        public List<OrganizationDto> GetOrganizations()
        {
            return _dbContext.Organizations.Select(org => OrganizationDto.CreateDtoFromOrg(org)).ToList();
        }

        public List<OrganizationDto> GetFilteredOrganizations(string name)
        {
             return _dbContext.Organizations
                 .Where(org => org.NameOfClub.Contains(name))
                 .Select(org => OrganizationDto.CreateDtoFromOrg(org)).ToList();
        }

        // Organization Data
        public OrganizationDetailDto GetOrganization(string name)
        {
            Organization org = _dbContext.Organizations
                .Include(org => org.ClubClassification)
                .Include(org => org.TechsyncName)
                .FirstOrDefault(org => org.NameOfClub.Equals(name));

            return OrganizationDetailDto.CreateDtoFromOrg(org);
        }
    }
}