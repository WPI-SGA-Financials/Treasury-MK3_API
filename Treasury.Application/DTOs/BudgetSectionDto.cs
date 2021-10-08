using System.Linq;
using Treasury.Domain.Models.Tables;

namespace Treasury.Application.DTOs
{
    public class BudgetSectionDto
    {
        public static BudgetSectionDto CreateDtoFromBudgetSection(BudgetSection section)
        {
            BudgetSectionDto dto = new BudgetSectionDto
            {
                Id = section.Id,
                Name = section.SectionName,
                NumOfItems = section.BudgetLineItems.Count,
                AmountRequested = section.BudgetLineItems.Sum(item => item.AmountRequest),
                AmountProposed = section.BudgetLineItems.Sum(item => item.AmountProposed),
                Appealed = section.BudgetLineItems.Any(item => item.Appealed == 1),
                RequestedAppeal = section.BudgetLineItems.Sum(item => item.AppealAmount),
                
                ApprovedAppeal = section.BudgetLineItems.Sum(item => item.ApprovedAppeal),
                
                AmountApproved = GetTotalApproved(
                    section.BudgetLineItems.Sum(item => item.AmountProposed), 
                    section.BudgetLineItems.Sum(item => item.ApprovedAppeal))
            };
            
            return dto;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfItems { get; set; }
        public decimal AmountRequested { get; set; }
        public decimal AmountProposed { get; set; }
        public bool Appealed { get; set; }
        public decimal RequestedAppeal { get; set; }
        public decimal ApprovedAppeal { get; set; }
        public decimal AmountApproved { get; set; }

        private static decimal GetTotalApproved(decimal amtProposed, decimal approvedAppeal)
        {
            return amtProposed + approvedAppeal;
        }
    }
}