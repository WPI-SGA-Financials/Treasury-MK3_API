using System.Collections.Generic;
using System.Linq;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.DTOs
{
    public class BudgetDto
    {
        public static BudgetDto CreateDtoFromBudget(Budget budget)
        {
            BudgetDto dto = new BudgetDto
            {
                Id = budget.Id,
                NameOfClub = budget.NameOfClub,
                FiscalYear = budget.FiscalYear
            };
            
            return dto;
        }
        
        public int Id { get; set; }
        public string NameOfClub { get; set; }
        public string FiscalYear { get; set; }
        public int NumOfItems { get; set; }
        public decimal AmountRequested { get; set; }
        public decimal AmountProposed { get; set; }
        public decimal AmountApproved { get; set; }
        
    }

    public class BudgetDetailedDto: BudgetDto
    {
        public static BudgetDetailedDto CreateDtoFromBudgetAndSection(Budget budget, List<BudgetSectionDto> budgetSections)
        {
            BudgetDetailedDto dto = new BudgetDetailedDto
            {
                Id = budget.Id,
                NameOfClub = budget.NameOfClub,
                FiscalYear = budget.FiscalYear,
                NumOfItems = budgetSections.Sum(sectionDto => sectionDto.NumOfItems),
                AmountApproved = budgetSections.Sum(sectionDto => sectionDto.AmountApproved),
                AmountProposed = budgetSections.Sum(sectionDto => sectionDto.AmountProposed),
                AmountRequested = budgetSections.Sum(sectionDto => sectionDto.AmountRequested),
                Appealed = budgetSections.Any(sectionDto => sectionDto.Appealed),
                AppealAmount = budgetSections.Sum(sectionDto => sectionDto.RequestedAppeal),
                AppealDecision = DetermineAppealDecision(budgetSections),
                ApprovedAppeal = budgetSections.Sum(sectionDto => sectionDto.ApprovedAppeal),
                Sections = budgetSections
            };

            return dto;
        }

        private static string DetermineAppealDecision(List<BudgetSectionDto> budgetSections)
        {
            decimal totalRequestedAppeal = budgetSections.Sum(dto => dto.RequestedAppeal);
            decimal totalApprovedAppeal = budgetSections.Sum(dto => dto.ApprovedAppeal);

            if (totalRequestedAppeal == 0)
            {
                return null;
            }

            if (totalRequestedAppeal == totalApprovedAppeal)
            {
                return "Passed in Full";
            }

            if (totalApprovedAppeal < totalRequestedAppeal && totalApprovedAppeal > 0)
            {
                return "Partially Passed";
            }

            return "Denied";
        }

        public static BudgetDetailedDto CreateDtoFromBudgetAndLegacy(Budget budget, BudgetLegacyDto legacy)
        {
            BudgetDetailedDto dto = new BudgetDetailedDto
            {
                Id = budget.Id,
                NameOfClub = budget.NameOfClub,
                FiscalYear = budget.FiscalYear,
                NumOfItems = -1,
                AmountApproved = legacy.AmountApproved,
                AmountProposed = legacy.AmountProposed,
                AmountRequested = legacy.AmountRequested,
                Appealed = legacy.Appealed,
                AppealAmount = legacy.AppealAmount,
                AppealDecision = legacy.AppealDecision,
                ApprovedAppeal = legacy.ApprovedAppeal
            };

            return dto;
        }
        
        #nullable enable
        public List<BudgetSectionDto>? Sections { get; set; }
        #nullable disable
        public bool Appealed { get; set; }
        public decimal AppealAmount { get; set; }
        public string AppealDecision { get; set; }
        public decimal ApprovedAppeal { get; set; }
    }
}