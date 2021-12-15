using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables;

[Table("Operating Expenses")]
[Index(nameof(MtliId), Name = "MT_Expenses_MT_LineItems_ID_fk")]
public partial class OperatingExpense
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("MTLI_ID")]
    public int MtliId { get; set; }

    public decimal Spent { get; set; }

    [Required]
    [StringLength(100)]
    public string Person { get; set; }

    [Required]
    [StringLength(255)]
    public string Description { get; set; }

    [Required]
    [Column("Payment Type")]
    [StringLength(20)]
    public string PaymentType { get; set; }

    [Column("Workday Approved", TypeName = "bit(1)")]
    public bool? WorkdayApproved { get; set; }

    [Column("Workday Approval Date", TypeName = "date")]
    public DateTime? WorkdayApprovalDate { get; set; }

    [StringLength(255)]
    public string Notes { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime Timestamp { get; set; }

    [ForeignKey(nameof(MtliId))]
    [InverseProperty(nameof(MtlineItem.OperatingExpenses))]
    public virtual MtlineItem Mtli { get; set; }
}