using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.Accessor
{
    public class FundingRequestAccessor
    {
        private readonly sgadbContext _dbContext;

        public FundingRequestAccessor(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // Organization Data
        public List<FundingRequestDto> GetFundingRequestsByOrganization(string organization)
        {
            List<FundingRequestDto> frs = _dbContext.FundingRequests
                .Where(fr=> fr.NameOfClub.Equals(organization.Trim()))
                .OrderByDescending(fr => fr.FundingDate)
                .ThenByDescending(fr => fr.DotNumber)
                .Select(fr => FundingRequestDto.CreateDtoFromFr(fr))
                .ToList();

            return frs.Any() ? frs : null;
        }
        
        public List<FundingRequestDto> GetFundingRequestsByOrganizationFy(string organization, int fy)
        {
            string fiscalYear = fy.ToString().PadLeft(2, '0');
            
            List<FundingRequestDto> frs = _dbContext.FundingRequests
                .Where(fr=> fr.NameOfClub.Equals(organization.Trim()))
                .Where(fr => fr.FiscalYear.Equals("FY " + fiscalYear))
                .OrderByDescending(fr => fr.FundingDate)
                .ThenByDescending(fr => fr.DotNumber)
                .Select(fr => FundingRequestDto.CreateDtoFromFr(fr))
                .ToList();

            return frs.Any() ? frs : null;
        }
        
        // Financials Data
        public List<FundingRequestDto> GetFundingRequests()
        {
            List<FundingRequestDto> frs = _dbContext.FundingRequests
                .OrderByDescending(fr => fr.FundingDate)
                .ThenByDescending(fr => fr.DotNumber)
                .Select(fr => FundingRequestDto.CreateDtoFromFr(fr))
                .ToList();

            return frs;
        }
        
        public List<FundingRequestDto> GetFundingRequestsByFy(int fy)
        {
            string fiscalYear = fy.ToString().PadLeft(2, '0');
            
            List<FundingRequestDto> frs = _dbContext.FundingRequests
                .Where(fr => fr.FiscalYear.Equals("FY " + fiscalYear))
                .OrderByDescending(fr => fr.FundingDate)
                .ThenByDescending(fr => fr.DotNumber)
                .Select(fr => FundingRequestDto.CreateDtoFromFr(fr))
                .ToList();

            return frs.Any() ? frs : null;
        }

        public FundingRequestDetailedDto GetFundingRequestById(int id)
        {
            FundingRequest fr = _dbContext.FundingRequests
                .Include(fr => fr.Frappeal)
                .FirstOrDefault(fr => fr.Id.Equals(id));
            
            return fr != null ? FundingRequestDetailedDto.CreateDtoFromFr(fr) : null;
        }
    }
}