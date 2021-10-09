using Treasury.Domain.Models.Tables;

namespace Treasury.Application.DTOs
{
    public class BudgetLegacyDto
    {
        public static BudgetLegacyDto CreateDtoFromLegacy(BudgetLegacy legacy)
        {
            BudgetLegacyDto dto = new BudgetLegacyDto
            {
                Id = legacy.Id,
                AmountRequested = legacy.AmountRequested,
                AmountProposed = legacy.AmountProposed,
                Appealed = legacy.Appealed == 1,
                AppealAmount = legacy.AppealAmount,
                AppealDecision = legacy.AppealDecision,
                ApprovedAppeal = legacy.ApprovedAppeal
            };

            return dto;
        }
        
        public int Id { get; set; }
        public decimal AmountRequested { get; set; }
        public decimal AmountProposed { get; set; }
        public bool Appealed { get; set; }
        public decimal AppealAmount { get; set; }
        public string AppealDecision { get; set; }
        public decimal ApprovedAppeal { get; set; }
        public decimal AmountApproved => AmountProposed + ApprovedAppeal;
    }
}