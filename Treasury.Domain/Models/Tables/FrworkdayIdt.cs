using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("FRWorkdayIDT")]
    [Index(nameof(FrId), Name = "FRWorkdayIDT_FR_ID_uindex", IsUnique = true)]
    public partial class FrworkdayIdt
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("FR_ID")]
        public int FrId { get; set; }
        [Column("IDT Submitted", TypeName = "bit(1)")]
        public bool? IdtSubmitted { get; set; }
        [Column("Workday Approved")]
        [StringLength(15)]
        public string WorkdayApproved { get; set; }
        [Column("Workday Approval Date", TypeName = "date")]
        public DateTime? WorkdayApprovalDate { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(FrId))]
        [InverseProperty(nameof(FundingRequest.FrworkdayIdt))]
        public virtual FundingRequest Fr { get; set; }
    }
}
