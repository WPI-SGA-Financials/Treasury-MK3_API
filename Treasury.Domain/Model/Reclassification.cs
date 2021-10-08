using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class Reclassification
    {
        public Reclassification()
        {
            ReclassMinutes = new HashSet<ReclassMinute>();
        }

        public int Id { get; set; }
        public string NameOfClub { get; set; }
        public DateTime HearingDate { get; set; }
        public string FiscalYear { get; set; }
        public string DotNumber { get; set; }
        public string OriginalClass { get; set; }
        public string RequestedClass { get; set; }
        public string Decision { get; set; }
        public string ApprovedClass { get; set; }
        public string Notes { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Organization NameOfClubNavigation { get; set; }
        public virtual ICollection<ReclassMinute> ReclassMinutes { get; set; }
    }
}
