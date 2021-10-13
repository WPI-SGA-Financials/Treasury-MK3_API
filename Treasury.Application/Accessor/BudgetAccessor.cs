using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Treasury.Application.Contexts;
using Treasury.Application.DTOs;
using Treasury.Application.Errors;
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
        public object GetBudgetByOrganization(string organization)
        {
            if (ValidateModel(organization) is { } validate)
            {
                return validate;
            }
            
            List<BudgetDto> budgets = _dbContext.BudgetByFys
                .Where(budget => budget.NameOfClub.Equals(organization.Trim()))
                .OrderBy(budget => budget.NameOfClub)
                .ThenByDescending(budget => budget.FiscalYear)
                .Select(budget => BudgetDto.CreateDtoFromBudget(budget))
                .ToList();

            if (budgets.Any())
            {
                return budgets;
            }

            var errorDict = new Dictionary<string, object>
            {
                { "nameOfClub", organization }
            };

            return new NotFoundError("The requested organizations budgets were not found", errorDict);
        }

        public object GetBudgetByOrganizationFy(string organization, int fy)
        {
            if (ValidateModel(organization, fy) is { } validate)
            {
                return validate;
            }
            
            string fiscalYear = fy.ToString().PadLeft(2, '0');
            
            BudgetDto budget = _dbContext.BudgetByFys
                .Where(b => b.NameOfClub.Equals(organization.Trim()))
                .Where(b => b.FiscalYear.Equals("FY " + fiscalYear))
                .OrderBy(b => b.NameOfClub)
                .ThenByDescending(b => b.FiscalYear)
                .Select(b => BudgetDto.CreateDtoFromBudget(b))
                .FirstOrDefault();

            if (budget != null)
            {
                return budget;
            }
            
            var errorDict = new Dictionary<string, object>
            {
                { "nameOfClub", organization },
                { "fiscalYear", fy }
            };

            return new NotFoundError("The requested organizations budgets were not found", errorDict);
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

        public object GetBudgetsByFy(int fy)
        {
            if (ValidateModel(fy: fy) is { } validate)
            {
                return validate;
            }
            
            string fiscalYear = fy.ToString().PadLeft(2, '0');
            
            List<BudgetDto> budgets = _dbContext.BudgetByFys
                .Where(budget => budget.FiscalYear.Equals("FY " + fiscalYear))
                .OrderBy(budget => budget.NameOfClub)
                .ThenByDescending(budget => budget.FiscalYear)
                .Select(budget => BudgetDto.CreateDtoFromBudget(budget))
                .ToList();
            
            if (budgets.Any())
            {
                return budgets;
            }
            
            var errorDict = new Dictionary<string, object>
            {
                { "fiscalYear", fy }
            };

            return new NotFoundError("The requested fiscal years budgets were not found", errorDict);
        }

        public object GetBudgetById(int id)
        {
            if (ValidateModel(id: id) is { } validate)
            {
                return validate;
            }
            
            Budget budget = _dbContext.Budgets.Find(id);

            if (budget == null)
            {
                var errorDict = new Dictionary<string, object>
                {
                    { "id", id }
                };

                return new NotFoundError("The requested budget was not found", errorDict);
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
                errorDict.Add("id", "Budget ID cannot be less than one");
            }

            return errorDict.Count > 0 ? new InvalidArgumentsError("One or more parameters is invalid", errorDict) : null;
        }
    }
}