using System.Collections.Generic;
using Treasury.Application.Contracts.V1.Requests;
using Treasury.Application.DTOs;

namespace Treasury.Application.Accessor.Interface;

public interface IBudgetAccessor
{
    List<BudgetDto> GetBudgetByOrganization(string organization);
    List<BudgetDto> GetBudgets(FinancialPagedRequest financialPagedRequest, out int maxResults);
    BudgetDetailedDto GetBudgetById(int id);
}