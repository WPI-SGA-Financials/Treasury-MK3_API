using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables;

[Index(nameof(From), Name = "transferFrom")]
[Index(nameof(To), Name = "transferTo_idx")]
public partial class Transfer
{
    [Key]
    [Column("Fiscal Year")]
    public string FiscalYear { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public decimal? Amount { get; set; }

    [Key]
    [Column("Date of Transfer", TypeName = "date")]
    public DateTime DateOfTransfer { get; set; }

    [StringLength(255)]
    public string Notes { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime Timestamp { get; set; }
}