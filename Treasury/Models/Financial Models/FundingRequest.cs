using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Treasury.Models.Financial_Models
{
    [Table("Complete Funding Request")]
    public class FundingRequest
    {
        [Column("ID")] 
        public int ID { get; set; }
        
        [Column("Name of Club")]
        public string NameOfClub { get; set; }

        [Column("Hearing Date")]
        public DateTime HearingDate { get; set; }
        
        [Column("Date of Event")]
        public DateTime? EventDate { get; set; }
        
        [Column("Dot Number")] 
        public string DotNumber { get; set; }
        
        [Column("Description")] 
        public string Description { get; set; }

        [Column("Fiscal Year")]
        public string FiscalYear { get; set; }

        [Column("Requested")]
        public decimal AmountRequested { get; set; }

        public string Decision { get; set; }

        [Column("Approved")]
        public decimal AmountApproved { get; set; }
        
        [Column("Appealed")] 
        public string Appealed { get; set; }
        
        [Column("Appeal Amount")] 
        public decimal? RequestedAppeal { get; set; }
        
        [Column("Appeal Decision")] 
        public string? AppealDecision { get; set; }
        
        [Column("Approved Appeal")] 
        public decimal? ApprovedAppeal { get; set; }
        
        [Column("Appeal Minutes")] 
        public string? AppealMinutes { get; set; }
        
    }
}
