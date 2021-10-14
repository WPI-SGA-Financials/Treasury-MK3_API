using System;
using System.Collections.Generic;
using System.Linq;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Application.Errors;
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
        public object GetReallocationRequestsByOrganization(string organization)
        {
            if (ValidateModel(organization) is { } validate)
            {
                return validate;
            }
            
            List<ReallocationRequestDto> reallocs = _dbContext.Reallocations
                .Where(realloc=> realloc.NameOfClub.Equals(organization))
                .OrderByDescending(realloc => realloc.HearingDate)
                .ThenByDescending(realloc => realloc.DotNumber)
                .Select(realloc => ReallocationRequestDto.CreateDtoFromRealloc(realloc))
                .ToList();

            if (reallocs.Any())
            {
                return reallocs;
            }

            var errorDict = new Dictionary<string, object>
            {
                { "nameOfClub", organization }
            };

            return new NotFoundError("The requested organizations reallocations were not found", errorDict);
        }
        
        public object GetReallocationRequestsByOrganizationFy(string organization, int fy)
        {
            if (ValidateModel(organization, fy) is { } validate)
            {
                return validate;
            }
            
            string fiscalYear = fy.ToString().PadLeft(2, '0');
            
            List<ReallocationRequestDto> reallocs = _dbContext.Reallocations
                .Where(realloc=> realloc.NameOfClub.Equals(organization))
                .Where(realloc => realloc.FiscalYear.Equals("FY " + fiscalYear))
                .OrderByDescending(realloc => realloc.HearingDate)
                .ThenByDescending(realloc => realloc.DotNumber)
                .Select(realloc => ReallocationRequestDto.CreateDtoFromRealloc(realloc))
                .ToList();

            if (reallocs.Any())
            {
                return reallocs;
            }
            
            var errorDict = new Dictionary<string, object>
            {
                { "nameOfClub", organization },
                { "fiscalYear", "FY " + fiscalYear }
            };

            return new NotFoundError("The requested organizations reallocations were not found", errorDict);
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
        
        public object GetReallocationRequestsByFy(int fy)
        {
            if (ValidateModel(fy: fy) is { } validate)
            {
                return validate;
            }
            
            string fiscalYear = fy.ToString().PadLeft(2, '0');
            
            List<ReallocationRequestDto> reallocs = _dbContext.Reallocations
                .Where(realloc => realloc.FiscalYear.Equals("FY " + fiscalYear))
                .OrderByDescending(realloc => realloc.HearingDate)
                .ThenByDescending(realloc => realloc.DotNumber)
                .Select(realloc => ReallocationRequestDto.CreateDtoFromRealloc(realloc))
                .ToList();

            if (reallocs.Any())
            {
                return reallocs;
            }
            
            var errorDict = new Dictionary<string, object>
            {
                { "fiscalYear", "FY " + fiscalYear }
            };

            return new NotFoundError("The requested reallocations for the fiscal year were not found", errorDict);
        }

        public object GetReallocationRequestById(int id)
        {
            if (ValidateModel(id: id) is { } validate)
            {
                return validate;
            }
            
            Reallocation realloc = _dbContext.Reallocations
                .FirstOrDefault(realloc => realloc.Id.Equals(id));

            if (realloc != null)
            {
                return ReallocationRequestDetailedDto.CreateDtoFromRealloc(realloc);
            }
            
            var errorDict = new Dictionary<string, object>
            {
                { "id", id }
            };

            return new NotFoundError("The requested reallocation was not found", errorDict);
        }
        
        private static InvalidArgumentsError ValidateModel(string organization = null, int? fy = null, int? id = null)
        {
            Dictionary<string, object> errorDict = new Dictionary<string, object>();
            
            if (organization != null && String.IsNullOrWhiteSpace(organization))
            {
                errorDict.Add("nameOfClub", "Organization name cannot be empty");
            }
            if (fy != null && fy is < 1 or > 99)
            {
                errorDict.Add("fy", "Fiscal Year is out of bounds");
            }

            if (id != null && id < 1)
            {
                errorDict.Add("id", "Reallocation ID cannot be less than one");
            }

            return errorDict.Count > 0 ? new InvalidArgumentsError("One or more parameters is invalid", errorDict) : null;
        }
    }
}