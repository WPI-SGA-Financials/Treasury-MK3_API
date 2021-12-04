using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Views
{
    [Keyless]
    public partial class Liability
    {
        [Column("Fiscal Year")]
        [StringLength(6)]
        public string FiscalYear { get; set; }
        [Column("Heard Requests")]
        public long HeardRequests { get; set; }
        [Column("Total Approved")]
        public decimal? TotalApproved { get; set; }
        [Column("RF Approved Amt")]
        public decimal? RfApprovedAmt { get; set; }
        [Column("Total Liability")]
        public decimal? TotalLiability { get; set; }
        [Column("Workday Approved Requests")]
        public decimal? WorkdayApprovedRequests { get; set; }
        [Column("Total Workday Liability")]
        public decimal? TotalWorkdayLiability { get; set; }
    }
}
