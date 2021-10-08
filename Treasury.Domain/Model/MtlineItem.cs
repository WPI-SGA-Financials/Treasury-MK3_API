using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class MtlineItem
    {
        public MtlineItem()
        {
            OperatingExpenses = new HashSet<OperatingExpense>();
        }

        public int Id { get; set; }
        public int MtId { get; set; }
        public string LineItem { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual MandatoryTransfer Mt { get; set; }
        public virtual ICollection<OperatingExpense> OperatingExpenses { get; set; }
    }
}
