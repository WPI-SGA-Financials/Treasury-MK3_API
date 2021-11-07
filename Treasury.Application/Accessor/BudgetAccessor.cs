using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Treasury.Application.Contexts;
using Treasury.Application.Contracts.V1.Requests;
using Treasury.Application.DTOs;
using Treasury.Application.Util;
using Treasury.Domain.Models.Tables;
using Treasury.Domain.Models.Views;

namespace Treasury.Application.Accessor
{
    public class BudgetAccessor
    {
        private readonly sgadbContext _dbContext;

        public BudgetAccessor(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Organization Data
        public List<BudgetDto> GetBudgetByOrganization(string organization)
        {
            List<BudgetDto> budgets = _dbContext.BudgetByFys
                .Where(budget => budget.NameOfClub.Equals(organization.Trim()))
                .OrderBy(budget => budget.NameOfClub)
                .ThenByDescending(budget => budget.FiscalYear)
                .Select(budget => BudgetDto.CreateDtoFromBudget(budget))
                .ToList();

            return budgets.Any() ? budgets : null;
        }

        // Financial Data
        public List<BudgetDto> GetBudgets(FinancialPagedRequest financialPagedRequest, out int maxResults)
        {
            int skip = HelperFunctions.GetPage(financialPagedRequest);

            DbSet<BudgetByFy> baseQuery = _dbContext.BudgetByFys;

            maxResults = baseQuery.Count();
            
            // TODO: Add in Filtering based on all available filters
            // TODO: Join in Organization Table to allow for filtering
            
            return baseQuery 
                .OrderBy(budget => budget.NameOfClub)
                .ThenByDescending(budget => budget.FiscalYear)
                .Skip(skip)
                .Take(financialPagedRequest.Rpp)
                .Select(budget => BudgetDto.CreateDtoFromBudget(budget))
                .ToList();
        }

        public BudgetDetailedDto GetBudgetById(int id)
        {
            Budget budget = _dbContext.Budgets.Find(id);

            if (budget == null)
            {
                return null;
            }

            BudgetLegacy legacy = _dbContext.BudgetLegacies
                .FirstOrDefault(budgetLegacy => budgetLegacy.BId.Equals(id));

            if (legacy != null)
            {
                return BudgetDetailedDto.CreateDtoFromBudgetAndLegacy(budget,
                    BudgetLegacyDto.CreateDtoFromLegacy(legacy));
            }

            List<BudgetSectionDto> budgetSections = _dbContext.BudgetSections
                .Where(section => section.BId.Equals(id))
                .Include(section => section.BudgetLineItems)
                .Select(section => BudgetSectionDto.CreateDtoFromBudgetSection(section))
                .ToList();

            return BudgetDetailedDto.CreateDtoFromBudgetAndSection(budget, budgetSections);
        }
    }
}