using System;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    public partial class Transfer
    {
        public string FiscalYear { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal? Amount { get; set; }
        public DateTime DateOfTransfer { get; set; }
        public string Notes { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
