#nullable disable

namespace Treasury.Domain.Models.Views
{
    public partial class MandatoryTransfersTotalBudget
    {
        public string FiscalYear { get; set; }
        public string ParentOrganization { get; set; }
        public string FundName { get; set; }
        public decimal TotalBudget { get; set; }
    }
}
