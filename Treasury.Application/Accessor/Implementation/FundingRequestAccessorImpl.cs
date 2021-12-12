using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Treasury.Application.Accessor.Interface;
using Treasury.Application.Contexts;
using Treasury.Application.Contracts.V1.Requests;
using Treasury.Application.DTOs;
using Treasury.Application.Util;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.Accessor.Implementation
{
    public class FundingRequestAccessorImpl : IFundingRequestAccessor
    {
        private readonly sgadbContext _dbContext;

        public FundingRequestAccessorImpl(sgadbContext dbContext)
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
            int skip = GeneralHelperFunctions.GetPage(financialPagedRequest);

            var baseQuery = _dbContext.FundingRequests;

            var filteredQuery = ApplyFilters(financialPagedRequest, baseQuery);
            
            maxResults = filteredQuery.Count();

            return filteredQuery
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

        
        private IQueryable<FundingRequest> ApplyFilters(FinancialPagedRequest request, DbSet<FundingRequest> baseQuery)
        {
            IQueryable<FundingRequest> filtered = baseQuery.Include(fundingRequest => fundingRequest.Organization);

            filtered = GeneralHelperFunctions.ApplyOrgBasedFilters(request, filtered);

            // Financial Based Filters
            if (request.FiscalYear.Length > 0)
            {
                var predicate = PredicateBuilder.False<FundingRequest>();

                predicate = request.FiscalYear.Aggregate(predicate, (current, fiscalYear) => current.Or(p => p.FiscalYear.Equals(fiscalYear)));

                filtered = filtered.Where(predicate);
            }
            
            if (request.Description.Length > 0)
            {
                var predicate = PredicateBuilder.False<FundingRequest>();

                predicate = request.Description.Aggregate(predicate, (current, description) => current.Or(p => p.Description.Contains(description)));

                filtered = filtered.Where(predicate);
            }
            
            if (request.MinimumRequestedAmount > 0)
            {
                filtered = filtered.Where(query => query.AmountRequested > request.MinimumRequestedAmount);
            }
            
            return filtered;
        }
    }
}