using System;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    public partial class BudgetLineItem
    {
        public int Id { get; set; }
        public int BsId { get; set; }
        public string LineItemName { get; set; }
        public decimal AmountRequest { get; set; }
        public decimal AmountProposed { get; set; }
        public ulong Appealed { get; set; }
        public decimal AppealAmount { get; set; }
        public string AppealDecision { get; set; }
        public decimal ApprovedAppeal { get; set; }
        public string Notes { get; set; }
        public decimal AmountSpent { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual BudgetSection Bs { get; set; }
    }
}
