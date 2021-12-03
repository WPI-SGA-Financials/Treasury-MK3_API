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
            IQueryable<FundingRequest> filtered = baseQuery.Include(fundingRequest => fundingRequest.NameOfClubNavigation);
            
            // TODO: Abstract Org Filters out
            // Organization Based Filters
            if (request.Name.Length > 0)
            {
                var predicate = PredicateBuilder.False<FundingRequest>();

                predicate = request.Name.Aggregate(predicate, (current, name) => current.Or(p => p.NameOfClub.Contains(name)));

                filtered = filtered.Where(predicate);
            }
            
            if (request.Acronym.Length > 0)
            {
                var predicate = PredicateBuilder.False<FundingRequest>();

                predicate = request.Acronym.Aggregate(predicate, (current, acronym) => current.Or(p => p.NameOfClubNavigation.Acronym1.Contains(acronym)));

                filtered = filtered.Where(predicate);
            }
            
            if (request.Classification.Length > 0)
            {
                var predicate = PredicateBuilder.False<FundingRequest>();
                
                predicate = request.Classification.Aggregate(predicate, (current, classification) => current.Or(p => p.NameOfClubNavigation.Classification.Contains(classification)));

                filtered = filtered.Where(predicate);
            }
            
            if (request.Type.Length > 0)
            {
                var predicate = PredicateBuilder.False<FundingRequest>();

                predicate = request.Type.Aggregate(predicate, (current, type) => current.Or(p => p.NameOfClubNavigation.TypeOfClub.Contains(type)));

                filtered = filtered.Where(predicate);
            }
            
            if (!request.IncludeInactive)
            {
                filtered = filtered.Where(query => !query.NameOfClubNavigation.Inactive);
            }

            // Financial Based Filters
            if (request.FiscalYear != -1)
            {
                filtered = filtered.Where(query => query.FiscalYear.Equals($"FY {request.FiscalYear}"));
            }
            
            if (request.Description.Length > 0)
            {
                var predicate = PredicateBuilder.False<FundingRequest>();

                predicate = request.Description.Aggregate(predicate, (current, description) => current.Or(p => p.Description.Contains(description)));

                filtered = filtered.Where(predicate);
            }
            
            if (request.RequestedAmount > 0)
            {
                filtered = filtered.Where(query => query.AmountRequested > request.RequestedAmount);
            }
            
            return filtered;
        }
    }
}