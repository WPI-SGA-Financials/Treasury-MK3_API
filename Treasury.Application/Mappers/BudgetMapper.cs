using System.Linq;
using Treasury.Application.DTOs;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.Mappers
{
    public static class BudgetMapper
    {
        public static BudgetDto FromBudgetLegacyToBudgetDto(Budget budget)
        {
            return new BudgetDto
            {
                Id = budget.Id,
                NameOfClub = budget.NameOfClub,
                FiscalYear = budget.FiscalYear,
                NumOfItems = -1,
                AmountRequested = budget.Legacy.AmountRequested,
                AmountProposed = budget.Legacy.AmountProposed,
                AmountApproved = budget.Legacy.AmountProposed + budget.Legacy.ApprovedAppeal
            };
        }
        
        public static BudgetDto FromBudgetSectionToBudgetDto(Budget budget)
        {
            BudgetDto dto = new BudgetDto
            {
                Id = budget.Id,
                NameOfClub = budget.NameOfClub,
                FiscalYear = budget.FiscalYear,
                NumOfItems = budget.Sections.Sum(section => section.BudgetLineItems.Count),
                AmountRequested = budget.Sections.Sum(section => section.BudgetLineItems.Sum(item => item.AmountRequest)),
                AmountProposed = budget.Sections.Sum(section => section.BudgetLineItems.Sum(item => item.AmountProposed))
            };

            dto.AmountApproved = dto.AmountProposed + budget.Sections.Sum(section => section.BudgetLineItems.Sum(item => item.ApprovedAppeal));

            return dto;
        }

        public static BudgetDetailedDto FromBudgetLegacyToBudgetDetailedDto(Budget budget)
        {
            BudgetDetailedDto budgetDetailedDto = new BudgetDetailedDto
            {
                Id = budget.Id,
                NameOfClub = budget.NameOfClub,
                FiscalYear = budget.FiscalYear,
                NumOfItems = -1,
                AmountProposed = budget.Legacy.AmountProposed,
                AmountRequested = budget.Legacy.AmountRequested,
                Appealed = budget.Legacy.Appealed,
                AppealAmount = budget.Legacy.AppealAmount,
                AppealDecision = budget.Legacy.AppealDecision,
                ApprovedAppeal = budget.Legacy.ApprovedAppeal,
                AmountApproved = budget.Legacy.AmountProposed + budget.Legacy.ApprovedAppeal
            };

            return budgetDetailedDto;
        }
        
        public static BudgetDetailedDto FromBudgetSectionToBudgetDetailedDto(Budget budget)
        {
            BudgetDetailedDto dto = new BudgetDetailedDto
            {
                Id = budget.Id,
                NameOfClub = budget.NameOfClub,
                FiscalYear = budget.FiscalYear,
                NumOfItems = budget.Sections.Sum(section => section.BudgetLineItems.Count),
                AmountRequested = budget.Sections.Sum(section => section.BudgetLineItems.Sum(item => item.AmountRequest)),
                AmountProposed = budget.Sections.Sum(section => section.BudgetLineItems.Sum(item => item.AmountProposed)),
                Appealed = budget.Sections.Any(section => section.BudgetLineItems.Any(item => item.Appealed)),
                AppealAmount = budget.Sections.Sum(section => section.BudgetLineItems.Sum(item => item.AppealAmount)),
                ApprovedAppeal = budget.Sections.Sum(section => section.BudgetLineItems.Sum(item => item.ApprovedAppeal))
            };

            dto.AppealDecision = DetermineAppealDecision(dto.AppealAmount, dto.ApprovedAppeal);
            
            dto.AmountApproved = dto.AmountProposed + budget.Sections.Sum(section => section.BudgetLineItems.Sum(item => item.ApprovedAppeal));

            dto.Sections =
                budget.Sections.Select(BudgetSectionMapper.FromBudgetSectionToBudgetSectionDto).ToList();
            
            return dto;
        }
        
        private static string DetermineAppealDecision(decimal requestedAppeal, decimal approvedAppeal)
        {
            if (requestedAppeal == 0)
            {
                return null;
            }

            if (requestedAppeal == approvedAppeal)
            {
                return "Passed in Full";
            }

            if (approvedAppeal < requestedAppeal && approvedAppeal > 0)
            {
                return "Partially Passed";
            }

            return "Denied";
        }
    }
}