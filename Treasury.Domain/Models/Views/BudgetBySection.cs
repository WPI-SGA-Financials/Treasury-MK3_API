#nullable disable

namespace Treasury.Domain.Models.Views
{
    public partial class BudgetBySection
    {
        public int BudgetId { get; set; }
        public string NameOfClub { get; set; }
        public string FiscalYear { get; set; }
        public string SectionName { get; set; }
        public long NumOfItems { get; set; }
        public decimal? AmountRequested { get; set; }
        public decimal? AmountProposed { get; set; }
        public int Appealed { get; set; }
        public decimal? RequestedAppeal { get; set; }
        public decimal? ApprovedAppeal { get; set; }
        public decimal? AmountApproved { get; set; }
        public decimal? AmountSpent { get; set; }
    }
}
