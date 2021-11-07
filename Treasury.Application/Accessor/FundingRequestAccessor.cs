using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Treasury.Application.Contexts;
using Treasury.Application.Contracts.V1.Requests;
using Treasury.Application.DTOs;
using Treasury.Application.Util;
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

        // Financials Data
        public List<FundingRequestDto> GetFundingRequests(FinancialPagedRequest financialPagedRequest, out int maxResults)
        {
            int skip = HelperFunctions.GetPage(financialPagedRequest);

            var baseQuery = _dbContext.FundingRequests;

            maxResults = baseQuery.Count();
            
            // TODO: Add in Filtering based on all available filters
            // TODO: Join in Organization Table to allow for filtering
            
            return baseQuery
                .OrderByDescending(fr => fr.FundingDate)
                .ThenByDescending(fr => fr.DotNumber)
                .Skip(skip)
                .Take(financialPagedRequest.Rpp)
                .Select(fr => FundingRequestDto.CreateDtoFromFr(fr))
                .ToList();
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