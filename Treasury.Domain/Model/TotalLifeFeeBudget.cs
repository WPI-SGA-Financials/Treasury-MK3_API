using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class TotalLifeFeeBudget
    {
        public string FiscalYear { get; set; }
        public decimal? Total { get; set; }
    }
}
