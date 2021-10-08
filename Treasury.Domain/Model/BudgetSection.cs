using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class BudgetSection
    {
        public BudgetSection()
        {
            BudgetLineItems = new HashSet<BudgetLineItem>();
        }

        public int Id { get; set; }
        public int BId { get; set; }
        public string SectionName { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Budget BIdNavigation { get; set; }
        public virtual ICollection<BudgetLineItem> BudgetLineItems { get; set; }
    }
}
