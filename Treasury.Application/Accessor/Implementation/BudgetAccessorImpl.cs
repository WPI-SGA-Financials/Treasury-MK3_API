using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Treasury.Application.Accessor.Interface;
using Treasury.Application.Contexts;
using Treasury.Application.Contracts.V1.Requests;
using Treasury.Application.DTOs;
using Treasury.Application.Mappers;
using Treasury.Application.Util;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.Accessor.Implementation;

public class BudgetAccessorImpl : IBudgetAccessor
{
    private readonly sgadbContext _dbContext;

    public BudgetAccessorImpl(sgadbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Organization Data
    public List<BudgetDto> GetBudgetByOrganization(string organization)
    {
        var budgets = _dbContext.Budgets
            .Include(budget => budget.Legacy)
            .Include(budget => budget.Sections)
            .ThenInclude(section => section.BudgetLineItems)
            .AsSingleQuery()
            .Where(budget => budget.NameOfClub.Equals(organization.Trim()))
            .OrderByDescending(budget => budget.FiscalYear)
            .Select(budget =>
                budget.Legacy != null
                    ? BudgetMapper.FromBudgetLegacyToBudgetDto(budget)
                    : BudgetMapper.FromBudgetSectionToBudgetDto(budget))
            .ToList();

        return budgets.Any() ? budgets : null;
    }

    // Financial Data
    public List<BudgetDto> GetBudgets(FinancialPagedRequest financialPagedRequest, out int maxResults)
    {
        var skip = GeneralHelperFunctions.GetPage(financialPagedRequest);

        IQueryable<Budget> budgets = _dbContext.Budgets
            .Include(budget => budget.Legacy)
            .Include(budget => budget.Sections)
            .ThenInclude(section => section.BudgetLineItems);

        var filteredQuery = ApplyFilters(financialPagedRequest, budgets);

        maxResults = filteredQuery.Count();

        var rawFilteredBudgets = filteredQuery
            .AsSingleQuery()
            .OrderBy(budget => budget.NameOfClub)
            .ThenByDescending(budget => budget.FiscalYear)
            .Skip(skip)
            .Take(financialPagedRequest.Rpp);

        return rawFilteredBudgets
            .Select(budget =>
                budget.Legacy != null
                    ? BudgetMapper.FromBudgetLegacyToBudgetDto(budget)
                    : BudgetMapper.FromBudgetSectionToBudgetDto(budget))
            .ToList();
    }

    public BudgetDetailedDto GetBudgetById(int id)
    {
        var queryable = _dbContext.Budgets
            .Include(budget => budget.Legacy)
            .Include(budget => budget.Sections)
            .ThenInclude(section => section.BudgetLineItems)
            .AsSingleQuery()
            .Where(budget => budget.Id == id);

        return queryable.Select(budget =>
                budget.Legacy != null
                    ? BudgetMapper.FromBudgetLegacyToBudgetDetailedDto(budget)
                    : BudgetMapper.FromBudgetSectionToBudgetDetailedDto(budget))
            .FirstOrDefault();


        /*if (budgetObj == null)
        {
            return null;
        }

        BudgetLegacy legacy = _dbContext.BudgetLegacies
            .FirstOrDefault(budgetLegacy => budgetLegacy.BId.Equals(id));

        if (legacy != null)
        {
            return BudgetDetailedDto.CreateDtoFromBudgetAndLegacy(budgetObj,
                BudgetLegacyDto.CreateDtoFromLegacy(legacy));
        }

        List<BudgetSectionDto> budgetSections = _dbContext.BudgetSections
            .Where(section => section.BId.Equals(id))
            .Include(section => section.BudgetLineItems)
            .Select(section => BudgetSectionDto.CreateDtoFromBudgetSection(section))
            .ToList();

        return BudgetDetailedDto.CreateDtoFromBudgetAndSection(budgetObj, budgetSections);*/
    }

    private IQueryable<Budget> ApplyFilters(FinancialPagedRequest request, IQueryable<Budget> baseQuery)
    {
        IQueryable<Budget> filtered = baseQuery.Include(fundingRequest => fundingRequest.Organization);

        filtered = GeneralHelperFunctions.ApplyOrgBasedFilters(request, filtered);

        // Financial Based Filters
        if (request.FiscalYear.Length > 0)
        {
            var predicate = PredicateBuilder.False<Budget>();

            predicate = request.FiscalYear.Aggregate(predicate,
                (current, fiscalYear) => current.Or(p => p.FiscalYear.Equals(fiscalYear)));

            filtered = filtered.Where(predicate);
        }

        if (request.FiscalClass.Length > 0)
        {
            var predicate = PredicateBuilder.False<Budget>();

            predicate = request.FiscalClass.Aggregate(predicate,
                (current, fiscalClass) => current.Or(p => p.Legacy != null
                    ? _dbContext.GetFiscalClass(p.Legacy.AmountProposed, p.Legacy.ApprovedAppeal).Equals(fiscalClass)
                    : _dbContext.GetFiscalClass(
                            p.Sections.Sum(section => section.BudgetLineItems.Sum(item => item.AmountProposed)),
                            p.Sections.Sum(section => section.BudgetLineItems.Sum(item => item.ApprovedAppeal)))
                        .Equals(fiscalClass)));

            filtered = filtered.Where(predicate);
        }

        return filtered;
    }
}