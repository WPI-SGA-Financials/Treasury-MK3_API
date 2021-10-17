using System.Collections.Generic;
using System.Linq;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.Accessor
{
    public class ReallocationRequestAccessor
    {
        private readonly sgadbContext _dbContext;

        public ReallocationRequestAccessor(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // Organization Data
        public List<ReallocationRequestDto> GetReallocationRequestsByOrganization(string organization)
        {
            List<ReallocationRequestDto> reallocs = _dbContext.Reallocations
                .Where(realloc=> realloc.NameOfClub.Equals(organization.Trim()))
                .OrderByDescending(realloc => realloc.HearingDate)
                .ThenByDescending(realloc => realloc.DotNumber)
                .Select(realloc => ReallocationRequestDto.CreateDtoFromRealloc(realloc))
                .ToList();

            return reallocs.Any() ? reallocs : null;
        }
        
        public List<ReallocationRequestDto> GetReallocationRequestsByOrganizationFy(string organization, int fy)
        {
            string fiscalYear = fy.ToString().PadLeft(2, '0');
            
            List<ReallocationRequestDto> reallocs = _dbContext.Reallocations
                .Where(realloc=> realloc.NameOfClub.Equals(organization.Trim()))
                .Where(realloc => realloc.FiscalYear.Equals("FY " + fiscalYear))
                .OrderByDescending(realloc => realloc.HearingDate)
                .ThenByDescending(realloc => realloc.DotNumber)
                .Select(realloc => ReallocationRequestDto.CreateDtoFromRealloc(realloc))
                .ToList();

            return reallocs.Any() ? reallocs : null;
        }
        
        // Financials Data
        public List<ReallocationRequestDto> GetReallocationRequests()
        {
            List<ReallocationRequestDto> reallocs = _dbContext.Reallocations
                .OrderByDescending(realloc => realloc.HearingDate)
                .ThenByDescending(realloc => realloc.DotNumber)
                .Select(realloc => ReallocationRequestDto.CreateDtoFromRealloc(realloc))
                .ToList();

            return reallocs;
        }
        
        public List<ReallocationRequestDto> GetReallocationRequestsByFy(int fy)
        {
            string fiscalYear = fy.ToString().PadLeft(2, '0');
            
            List<ReallocationRequestDto> reallocs = _dbContext.Reallocations
                .Where(realloc => realloc.FiscalYear.Equals("FY " + fiscalYear))
                .OrderByDescending(realloc => realloc.HearingDate)
                .ThenByDescending(realloc => realloc.DotNumber)
                .Select(realloc => ReallocationRequestDto.CreateDtoFromRealloc(realloc))
                .ToList();

            return reallocs.Any() ? reallocs : null;
        }

        public ReallocationRequestDetailedDto GetReallocationRequestById(int id)
        {
            Reallocation realloc = _dbContext.Reallocations
                .FirstOrDefault(realloc => realloc.Id.Equals(id));

            return realloc != null ? ReallocationRequestDetailedDto.CreateDtoFromRealloc(realloc) : null;
        }
    }
}