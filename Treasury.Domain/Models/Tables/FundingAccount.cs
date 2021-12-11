using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("Funding Accounts")]
    public partial class FundingAccount
    {
        [Key]
        [Column("Account Name")]
        public string AccountName { get; set; }

        [Key]
        [Column("Fiscal Year")]
        public string FiscalYear { get; set; }

        [Column("Fall Transfer")]
        public decimal? FallTransfer { get; set; }

        [Column("Spring Transfer")]
        public decimal? SpringTransfer { get; set; }

        [Required]
        [Column("Work Day Code")]
        [StringLength(8)]
        public string WorkDayCode { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }
    }
}