using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class MandatoryTransfersTotalBudget
    {
        public string FiscalYear { get; set; }
        public string ParentOrganization { get; set; }
        public string FundName { get; set; }
        public decimal TotalBudget { get; set; }
    }
}
