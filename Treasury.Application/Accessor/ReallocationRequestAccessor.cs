﻿using System.Collections.Generic;
using System.Linq;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.Accessor
{
    public class ReallocationRequestAccessor
    {
        private sgadbContext _dbContext;

        public ReallocationRequestAccessor(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // Organization Data
        public List<ReallocationRequestDto> GetReallocationRequestsByOrganization(string organization)
        {
            List<ReallocationRequestDto> reallocs = _dbContext.Reallocations
                .Where(realloc=> realloc.NameOfClub.Contains(organization))
                .Select(realloc => ReallocationRequestDto.CreateDtoFromRealloc(realloc))
                .ToList();

            return reallocs;
        }
        
        public List<ReallocationRequestDto> GetReallocationRequestsByOrganizationFy(string organization, int fy)
        {
            List<ReallocationRequestDto> reallocs = _dbContext.Reallocations
                .Where(realloc=> realloc.NameOfClub.Contains(organization))
                .Where(realloc => realloc.FiscalYear.Contains(""+fy))
                .Select(realloc => ReallocationRequestDto.CreateDtoFromRealloc(realloc))
                .ToList();

            return reallocs;
        }
        
        // Financials Data
        public List<ReallocationRequestDto> GetReallocationRequests()
        {
            List<ReallocationRequestDto> reallocs = _dbContext.Reallocations
                .Select(realloc => ReallocationRequestDto.CreateDtoFromRealloc(realloc))
                .ToList();

            return reallocs;
        }
        
        public List<ReallocationRequestDto> GetReallocationRequestsByFy(int fy)
        {
            List<ReallocationRequestDto> reallocs = _dbContext.Reallocations
                .Where(realloc => realloc.FiscalYear.Contains(""+fy))
                .Select(realloc => ReallocationRequestDto.CreateDtoFromRealloc(realloc))
                .ToList();

            return reallocs;
        }

        public ReallocationRequestDetailedDto GetReallocationRequestById(int id)
        {
            Reallocation realloc = _dbContext.Reallocations
                .FirstOrDefault(realloc => realloc.Id == id);
            
            return ReallocationRequestDetailedDto.CreateDtoFromRealloc(realloc);
        }
    }
}