#nullable disable

namespace Treasury.Domain.Models.Views
{
    public partial class BudgetLineItemsReadable
    {
        public string NameOfClub { get; set; }
        public string FiscalYear { get; set; }
        public string SectionName { get; set; }
        public string LineItemName { get; set; }
        public decimal AmountRequested { get; set; }
        public decimal AmountProposed { get; set; }
        public decimal ApprovedAppeal { get; set; }
        public decimal AmountSpent { get; set; }
    }
}
