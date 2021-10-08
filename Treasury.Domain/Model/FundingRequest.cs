using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class FundingRequest
    {
        public FundingRequest()
        {
            Frsupplementals = new HashSet<Frsupplemental>();
        }

        public int Id { get; set; }
        public string NameOfClub { get; set; }
        public string Description { get; set; }
        public DateTime FundingDate { get; set; }
        public string FiscalYear { get; set; }
        public DateTime? DateOfEvent { get; set; }
        public string DotNumber { get; set; }
        public decimal AmountRequested { get; set; }
        public string Decision { get; set; }
        public decimal AmountApproved { get; set; }
        public string Notes { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Organization NameOfClubNavigation { get; set; }
        public virtual Frappeal Frappeal { get; set; }
        public virtual Frminute Frminute { get; set; }
        public virtual FrreportForm FrreportForm { get; set; }
        public virtual FrworkdayIdt FrworkdayIdt { get; set; }
        public virtual ICollection<Frsupplemental> Frsupplementals { get; set; }
    }
}
