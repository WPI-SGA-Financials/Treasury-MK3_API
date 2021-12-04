using System.Collections.Generic;
using System.Linq;
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
                .Where(realloc=> realloc.NameOfClub.Equals(organization.Trim()))
                .OrderByDescending(realloc => realloc.HearingDate)
                .ThenByDescending(realloc => realloc.DotNumber)
                .Select(realloc => ReallocationRequestDto.CreateDtoFromRealloc(realloc))
                .ToList();

            return reallocs.Any() ? reallocs : null;
        }
        
        
        // Financials Data
        public List<ReallocationRequestDto> GetReallocationRequests(FinancialPagedRequest financialPagedRequest, out int maxResults)
        {
            int skip = HelperFunctions.GetPage(financialPagedRequest);

            var baseQuery = _dbContext.Reallocations;
            
            maxResults = baseQuery.Count();
            
            // TODO: Add in Filtering based on all available filters
            // TODO: Join in Organization Table to allow for filtering
            
           return baseQuery
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
    }
}