using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Domain.Models.Tables;

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

        public BudgetDto GetBudgetByOrganizationFy(string organization, int fy)
        {
            string fiscalYear = fy.ToString().PadLeft(2, '0');
            
            BudgetDto budget = _dbContext.BudgetByFys
                .Where(b => b.NameOfClub.Equals(organization.Trim()))
                .Where(b => b.FiscalYear.Equals("FY " + fiscalYear))
                .OrderBy(b => b.NameOfClub)
                .ThenByDescending(b => b.FiscalYear)
                .Select(b => BudgetDto.CreateDtoFromBudget(b))
                .FirstOrDefault();

            return budget;
        }
        
        // Financial Data
        public List<BudgetDto> GetBudgets()
        {
            List<BudgetDto> budgets = _dbContext.BudgetByFys
                .OrderBy(budget => budget.NameOfClub)
                .ThenByDescending(budget => budget.FiscalYear)
                .Select(budget => BudgetDto.CreateDtoFromBudget(budget))
                .ToList();

            return budgets;
        }

        public List<BudgetDto> GetBudgetsByFy(int fy)
        {
            string fiscalYear = fy.ToString().PadLeft(2, '0');
            
            List<BudgetDto> budgets = _dbContext.BudgetByFys
                .Where(budget => budget.FiscalYear.Equals("FY " + fiscalYear))
                .OrderBy(budget => budget.NameOfClub)
                .ThenByDescending(budget => budget.FiscalYear)
                .Select(budget => BudgetDto.CreateDtoFromBudget(budget))
                .ToList();
            
            return budgets.Any() ? budgets : null;
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