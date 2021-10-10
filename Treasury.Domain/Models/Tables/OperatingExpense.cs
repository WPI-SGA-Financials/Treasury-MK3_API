using System;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    public partial class OperatingExpense
    {
        public int Id { get; set; }
        public int MtliId { get; set; }
        public decimal Spent { get; set; }
        public string Person { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; }
        public ulong? WorkdayApproved { get; set; }
        public DateTime? WorkdayApprovalDate { get; set; }
        public string Notes { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual MtlineItem Mtli { get; set; }
    }
}
