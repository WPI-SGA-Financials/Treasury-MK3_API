using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class BudgetLegacy
    {
        public int Id { get; set; }
        public int BId { get; set; }
        public decimal AmountRequested { get; set; }
        public decimal AmountProposed { get; set; }
        public ulong Appealed { get; set; }
        public decimal AppealAmount { get; set; }
        public string AppealDecision { get; set; }
        public decimal ApprovedAppeal { get; set; }
        public decimal AmountSpent { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Budget BIdNavigation { get; set; }
    }
}
