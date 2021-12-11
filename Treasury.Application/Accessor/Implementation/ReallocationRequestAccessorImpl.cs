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
    public class ReallocationRequestAccessorImpl : IReallocationRequestAccessor
    {
        private readonly sgadbContext _dbContext;

        public ReallocationRequestAccessorImpl(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Organization Data
        public List<ReallocationRequestDto> GetReallocationRequestsByOrganization(string organization)
        {
            List<ReallocationRequestDto> reallocs = _dbContext.Reallocations
                .Where(realloc => realloc.NameOfClub.Equals(organization.Trim()))
                .OrderByDescending(realloc => realloc.HearingDate)
                .ThenByDescending(realloc => realloc.DotNumber)
                .Select(realloc => ReallocationRequestDto.CreateDtoFromRealloc(realloc))
                .ToList();

            return reallocs.Any() ? reallocs : null;
        }


        // Financials Data
        public List<ReallocationRequestDto> GetReallocationRequests(FinancialPagedRequest financialPagedRequest,
            out int maxResults)
        {
            int skip = GeneralHelperFunctions.GetPage(financialPagedRequest);

            var baseQuery = _dbContext.Reallocations;

            var filteredQuery = ApplyFilters(financialPagedRequest, baseQuery);

            maxResults = filteredQuery.Count();

            return filteredQuery
                .OrderByDescending(realloc => realloc.HearingDate)
                .ThenByDescending(realloc => realloc.DotNumber)
                .Skip(skip)
                .Take(financialPagedRequest.Rpp)
                .Select(realloc => ReallocationRequestDto.CreateDtoFromRealloc(realloc))
                .ToList();
        }

        public ReallocationRequestDetailedDto GetReallocationRequestById(int id)
        {
            Reallocation realloc = _dbContext.Reallocations
                .FirstOrDefault(realloc => realloc.Id.Equals(id));

            return realloc != null ? ReallocationRequestDetailedDto.CreateDtoFromRealloc(realloc) : null;
        }

        private IQueryable<Reallocation> ApplyFilters(FinancialPagedRequest request, IQueryable<Reallocation> baseQuery)
        {
            IQueryable<Reallocation> filtered = baseQuery.Include(reallocation => reallocation.Organization);
            
            if (request.Name.Length > 0)
            {
                var predicate = PredicateBuilder.False<Reallocation>();

                predicate = request.Name.Aggregate(predicate,
                    (current, name) => current.Or(p => p.NameOfClub.Contains(name)));

                filtered = filtered.Where(predicate);
            }

            filtered = GeneralHelperFunctions.ApplyOrgBasedFilters(request, filtered);

            // Financial Based Filters
            if (request.FiscalYear.Length > 0)
            {
                var predicate = PredicateBuilder.False<Reallocation>();

                predicate = request.FiscalYear.Aggregate(predicate,
                    (current, fiscalYear) => current.Or(p => p.FiscalYear.Equals(fiscalYear)));

                filtered = filtered.Where(predicate);
            }

            if (request.Description.Length > 0)
            {
                var predicate = PredicateBuilder.False<Reallocation>();

                predicate = request.Description.Aggregate(predicate,
                    (current, description) => current.Or(p => p.Description.Contains(description)));

                filtered = filtered.Where(predicate);
            }

            return filtered;
        }
    }
}