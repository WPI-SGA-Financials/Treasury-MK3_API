using System;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    public partial class FundingAccount
    {
        public string AccountName { get; set; }
        public string FiscalYear { get; set; }
        public decimal? FallTransfer { get; set; }
        public decimal? SpringTransfer { get; set; }
        public string WorkDayCode { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
