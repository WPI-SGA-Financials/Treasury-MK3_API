using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class BudgetByFy
    {
        public int BudgetId { get; set; }
        public string NameOfClub { get; set; }
        public string FiscalYear { get; set; }
        public decimal? NumOfItems { get; set; }
        public decimal? AmountRequested { get; set; }
        public decimal? AmountProposed { get; set; }
        public decimal Appealed { get; set; }
        public decimal? RequestedAppeal { get; set; }
        public decimal? ApprovedAppeal { get; set; }
        public decimal? AmountApproved { get; set; }
        public decimal? AmountSpent { get; set; }
    }
}
