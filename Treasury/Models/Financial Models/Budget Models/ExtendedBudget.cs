using System.Collections.Generic;

namespace Treasury.Models.Financial_Models.Budget_Models
{
    public class ExtendedBudget
    {
        private ExtendedBudget(Budget budget)
        {
            ID = budget.ID;
            NameOfClub = budget.NameOfClub;
            FiscalYear = budget.FiscalYear;
            NumOfItems = budget.NumOfItems;
            AmountRequested = budget.AmountRequested;
            AmountProposed = budget.AmountProposed;
            AmountApproved = budget.AmountApproved;
            AmountSpent = budget.AmountSpent;
        }
        
        public int ID { get; set; }
        
        public string NameOfClub { get; set; }
        
        public string FiscalYear { get; set; }
        
        public int NumOfItems { get; set; }
        
        public decimal AmountRequested { get; set; }
        
        public decimal AmountProposed { get; set; }
        
        public decimal ApprovedAppeal { get; set; }
        
        public decimal AmountApproved { get; set; }
        
        public decimal AmountSpent { get; set; }
        
        public List<BudgetSection> BudgetSections { get; set; }

        public static ExtendedBudget createFromBudget(Budget budget)
        {
            return new ExtendedBudget(budget);
        }
    }
}