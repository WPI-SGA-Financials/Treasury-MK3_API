using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class Reallocation
    {
        public Reallocation()
        {
            ReallocMinutes = new HashSet<ReallocMinute>();
        }

        public int Id { get; set; }
        public string NameOfClub { get; set; }
        public string Description { get; set; }
        public DateTime HearingDate { get; set; }
        public string FiscalYear { get; set; }
        public string DotNumber { get; set; }
        public string AllocatedFrom { get; set; }
        public string AllocatedTo { get; set; }
        public decimal AllocationAmount { get; set; }
        public string Decision { get; set; }
        public decimal AmountApproved { get; set; }
        public string Notes { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Organization NameOfClubNavigation { get; set; }
        public virtual ICollection<ReallocMinute> ReallocMinutes { get; set; }
    }
}
