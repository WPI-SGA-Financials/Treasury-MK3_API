using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Views;

[Keyless]
public partial class AllOperatingExpense
{
    [Column("Fiscal Year")]
    [StringLength(255)]
    public string FiscalYear { get; set; }

    [Required]
    [Column("Line Item")]
    [StringLength(255)]
    public string LineItem { get; set; }

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
}