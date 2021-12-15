using System.Linq;
using Treasury.Application.DTOs;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.Mappers;

public class BudgetSectionMapper
{
    public static BudgetSectionDto FromBudgetSectionToBudgetSectionDto(BudgetSection section)
    {
        var dto = new BudgetSectionDto
        {
            Id = section.Id,
            Name = section.SectionName,
            NumOfItems = section.BudgetLineItems.Count,
            AmountRequested = section.BudgetLineItems.Sum(item => item.AmountRequest),
            AmountProposed = section.BudgetLineItems.Sum(item => item.AmountProposed),
            Appealed = section.BudgetLineItems.Any(item => item.Appealed),
            RequestedAppeal = section.BudgetLineItems.Sum(item => item.AppealAmount),

            ApprovedAppeal = section.BudgetLineItems.Sum(item => item.ApprovedAppeal)
        };

        return dto;
    }
}