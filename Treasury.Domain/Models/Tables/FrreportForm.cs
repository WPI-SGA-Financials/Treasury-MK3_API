using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables;

[Table("FRReportForms")]
[Index(nameof(FrId), Name = "FRReportForms_FR_ID_uindex", IsUnique = true)]
public partial class FrreportForm
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("FR_ID")]
    public int FrId { get; set; }

    [Column("Spent Amount")]
    public decimal? SpentAmount { get; set; }

    [StringLength(25)]
    public string Status { get; set; }

    [Column("Approved Amount")]
    public decimal? ApprovedAmount { get; set; }

    [Column("Approved Date", TypeName = "date")]
    public DateTime? ApprovedDate { get; set; }

    [StringLength(255)]
    public string Notes { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime Timestamp { get; set; }

    [ForeignKey(nameof(FrId))]
    [InverseProperty(nameof(FundingRequest.FrreportForm))]
    public virtual FundingRequest Fr { get; set; }
}