﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.Accessor
{
    public class FundingRequestAccessor
    {
        private sgadbContext _dbContext;

        public FundingRequestAccessor(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // Organization Data
        public List<FundingRequestDto> GetFundingRequestsByOrganization(string organization)
        {
            List<FundingRequestDto> frs = _dbContext.FundingRequests
                .Where(fr=> fr.NameOfClub.Contains(organization))
                .Select(fr => FundingRequestDto.CreateDtoFromFr(fr))
                .ToList();

            return frs;
        }
        
        public List<FundingRequestDto> GetFundingRequestsByOrganizationFy(string organization, int fy)
        {
            List<FundingRequestDto> frs = _dbContext.FundingRequests
                .Where(fr=> fr.NameOfClub.Contains(organization))
                .Where(fr => fr.FiscalYear.Contains(""+fy))
                .Select(fr => FundingRequestDto.CreateDtoFromFr(fr))
                .ToList();

            return frs;
        }
        
        // Financials Data
        public List<FundingRequestDto> GetFundingRequests()
        {
            List<FundingRequestDto> frs = _dbContext.FundingRequests
                .Select(fr => FundingRequestDto.CreateDtoFromFr(fr))
                .ToList();

            return frs;
        }
        
        public List<FundingRequestDto> GetFundingRequestsByFy(int fy)
        {
            List<FundingRequestDto> frs = _dbContext.FundingRequests
                .Where(fr => fr.FiscalYear.Contains(""+fy))
                .Select(fr => FundingRequestDto.CreateDtoFromFr(fr))
                .ToList();

            return frs;
        }

        public FundingRequestDetailedDto GetFundingRequestById(int id)
        {
            FundingRequest fr = _dbContext.FundingRequests
                .Include(fr => fr.Frappeal)
                .FirstOrDefault(fr => fr.Id == id);
            
            return FundingRequestDetailedDto.CreateDtoFromFr(fr);
        }
    }
}