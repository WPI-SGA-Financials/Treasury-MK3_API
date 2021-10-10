#nullable disable

namespace Treasury.Domain.Models.Views
{
    public partial class Liability
    {
        public string FiscalYear { get; set; }
        public long HeardRequests { get; set; }
        public decimal? TotalApproved { get; set; }
        public decimal? RfApprovedAmt { get; set; }
        public decimal? TotalLiability { get; set; }
        public decimal? WorkdayApprovedRequests { get; set; }
        public decimal? TotalWorkdayLiability { get; set; }
    }
}
