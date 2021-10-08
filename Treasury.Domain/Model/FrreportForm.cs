using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class FrreportForm
    {
        public int Id { get; set; }
        public int FrId { get; set; }
        public decimal? SpentAmount { get; set; }
        public string Status { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string Notes { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual FundingRequest Fr { get; set; }
    }
}
