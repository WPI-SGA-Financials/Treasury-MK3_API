using System;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    public partial class Frsupplemental
    {
        public int Id { get; set; }
        public int FrId { get; set; }
        public string ItemType { get; set; }
        public string OtherType { get; set; }
        public decimal AmountRequested { get; set; }
        public ulong? Amended { get; set; }
        public decimal? AmendedAmount { get; set; }
        public string Notes { get; set; }
        public DateTime? Timestamp { get; set; }

        public virtual FundingRequest Fr { get; set; }
    }
}
