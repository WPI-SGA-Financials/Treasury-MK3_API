using System;
using System.Text.Json.Serialization;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    public partial class Frappeal
    {
        public int Id { get; set; }
        public int FrId { get; set; }
        public string NewDotNumber { get; set; }
        public DateTime AppealDate { get; set; }
        public string Description { get; set; }
        public decimal AppealAmount { get; set; }
        public string Decision { get; set; }
        public decimal ApprovedAppeal { get; set; }
        public string Notes { get; set; }
        public string MinutesLink { get; set; }
        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public virtual FundingRequest Fr { get; set; }
    }
}
