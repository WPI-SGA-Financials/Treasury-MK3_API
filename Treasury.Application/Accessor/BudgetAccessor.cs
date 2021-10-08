using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.Accessor
{
    public class BudgetAccessor
    {
        private sgadbContext _dbContext;

        public BudgetAccessor(sgadbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // Organization Data
        
        
        
        // Financial Data
        public List<BudgetDto> GetBudgets()
        {
            List<BudgetDto> budgets = _dbContext.Budgets
                .OrderBy(budget => budget.NameOfClub)
                .ThenByDescending(budget => budget.FiscalYear)
                .Select(budget => BudgetDto.CreateDtoFromBudget(budget))
                .ToList();

            return budgets;
        }
        
        public List<BudgetDto> GetBudgetsByFy(int fy)
        {
            List<BudgetDto> budgets = _dbContext.Budgets
                .Where(budget => budget.FiscalYear.Contains(""+fy))
                .OrderBy(budget => budget.NameOfClub)
                .ThenByDescending(budget => budget.FiscalYear)
                .Select(budget => BudgetDto.CreateDtoFromBudget(budget))
                .ToList();

            return budgets;
        }

        public BudgetDetailedDto GetBudgetById(int id)
        {
            Budget budget = _dbContext.Budgets.Find(id);

            BudgetLegacy legacy = _dbContext.BudgetLegacies
                .FirstOrDefault(budgetLegacy => budgetLegacy.BId.Equals(id));

            if (legacy != null )
            {
                return BudgetDetailedDto.CreateDtoFromBudgetAndLegacy(budget, BudgetLegacyDto.CreateDtoFromLegacy(legacy));
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