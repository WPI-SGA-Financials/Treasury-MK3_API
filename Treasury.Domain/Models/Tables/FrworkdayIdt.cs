using System;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    public partial class FrworkdayIdt
    {
        public int Id { get; set; }
        public int FrId { get; set; }
        public ulong? IdtSubmitted { get; set; }
        public string WorkdayApproved { get; set; }
        public DateTime? WorkdayApprovalDate { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual FundingRequest Fr { get; set; }
    }
}
