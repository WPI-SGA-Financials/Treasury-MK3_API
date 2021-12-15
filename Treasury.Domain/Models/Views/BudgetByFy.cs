using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Views;

[Keyless]
public partial class BudgetByFy
{
    [Column("Budget ID")]
    public int BudgetId { get; set; }

    [Required]
    [Column("Name of Club")]
    [StringLength(255)]
    public string NameOfClub { get; set; }

    [Required]
    [Column("Fiscal Year")]
    [StringLength(6)]
    public string FiscalYear { get; set; }

    [Column("Num of Items")]
    public int NumOfItems { get; set; }

    [Column("Amount Requested")]
    public decimal AmountRequested { get; set; }

    [Column("Amount Proposed")]
    public decimal AmountProposed { get; set; }

    public decimal Appealed { get; set; }

    [Column("Requested Appeal")]
    public decimal? RequestedAppeal { get; set; }

    [Column("Approved Appeal")]
    public decimal? ApprovedAppeal { get; set; }

    [Column("Amount Approved")]
    public decimal AmountApproved { get; set; }

    [Column("Amount Spent")]
    public decimal? AmountSpent { get; set; }
}