using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class Soc
    {
        public int Id { get; set; }
        public string NameOfClub { get; set; }
        public string Acronym { get; set; }
        public DateTime HearingDate { get; set; }
        public string FiscalYear { get; set; }
        public string TypeOfClub { get; set; }
        public string PresidentEmail { get; set; }
        public string TreasurerEmail { get; set; }
        public int? ProjectedActiveMembers { get; set; }
        public string Decision { get; set; }
        public string Notes { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
