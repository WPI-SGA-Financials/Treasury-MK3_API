using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Treasury.Models.Financial_Models
{
    [Table("Complete Funding Request")]
    [Keyless]
    public class FundingRequest
    {
        [Column("Name of Club")]
        public string NameOfClub { get; set; }

        [Column("Hearing Date")]
        public DateTime HearingDate { get; set; }

        [Column("Fiscal Year")]
        public string FiscalYear { get; set; }

        [Column("Requested")]
        public decimal AmountRequested { get; set; }

        public string Decision { get; set; }

        [Column("Approved")]
        public decimal AmountApproved { get; set; }
    }
}
