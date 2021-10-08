using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class MandatoryTransfer
    {
        public MandatoryTransfer()
        {
            MtlineItems = new HashSet<MtlineItem>();
        }

        public int Id { get; set; }
        public string ParentOrganization { get; set; }
        public string FundName { get; set; }
        public string FiscalYear { get; set; }
        public string Worktag { get; set; }
        public decimal AmountRequested { get; set; }
        public decimal AmountProposed { get; set; }
        public decimal AmountApproved { get; set; }
        public string Notes { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Organization ParentOrganizationNavigation { get; set; }
        public virtual ICollection<MtlineItem> MtlineItems { get; set; }
    }
}
