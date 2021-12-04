using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Views
{
    [Keyless]
    public partial class CompleteFundingRequest
    {
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("Name of Club")]
        [StringLength(255)]
        public string NameOfClub { get; set; }
        [Column("Hearing Date", TypeName = "date")]
        public DateTime HearingDate { get; set; }
        [Column("Fiscal Year")]
        [StringLength(5)]
        public string FiscalYear { get; set; }
        [Column("Agenda Number")]
        [StringLength(9)]
        public string AgendaNumber { get; set; }
        [Required]
        [Column("Dot Number")]
        [StringLength(6)]
        public string DotNumber { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Column("Date of Event", TypeName = "date")]
        public DateTime? DateOfEvent { get; set; }
        public decimal Requested { get; set; }
        [Required]
        [StringLength(20)]
        public string Decision { get; set; }
        public decimal Approved { get; set; }
        [StringLength(512)]
        public string Notes { get; set; }
        [Column("Minutes Link")]
        [StringLength(255)]
        public string MinutesLink { get; set; }
        [Required]
        [StringLength(3)]
        public string Appealed { get; set; }
        [Column("Appeal Amount")]
        public decimal? AppealAmount { get; set; }
        [Column("Appeal Decision")]
        [StringLength(20)]
        public string AppealDecision { get; set; }
        [Column("Approved Appeal")]
        public decimal? ApprovedAppeal { get; set; }
        [Column("Appeal Minutes")]
        [StringLength(255)]
        public string AppealMinutes { get; set; }
        [Column("Spent Amount")]
        public decimal? SpentAmount { get; set; }
        [Column("Report Form Status")]
        [StringLength(25)]
        public string ReportFormStatus { get; set; }
        [Column("Report Form Approved Amount")]
        public decimal? ReportFormApprovedAmount { get; set; }
        [Column("IDT Submitted", TypeName = "bit(1)")]
        public bool? IdtSubmitted { get; set; }
        [Column("Workday Approved")]
        [StringLength(15)]
        public string WorkdayApproved { get; set; }
        [Column("Workday Approval Date", TypeName = "date")]
        public DateTime? WorkdayApprovalDate { get; set; }
    }
}
