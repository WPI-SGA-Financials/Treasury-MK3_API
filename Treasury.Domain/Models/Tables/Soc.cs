using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Treasury.Domain.Models.Tables;

[Table("SOC")]
public partial class Soc
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [Column("Name of Club")]
    [StringLength(255)]
    public string NameOfClub { get; set; }

    [StringLength(255)]
    public string Acronym { get; set; }

    [Column("Hearing Date", TypeName = "date")]
    public DateTime HearingDate { get; set; }

    [Required]
    [Column("Fiscal Year")]
    [StringLength(255)]
    public string FiscalYear { get; set; }

    [Required]
    [Column("Type of Club")]
    [StringLength(255)]
    public string TypeOfClub { get; set; }

    [Column("President Email")]
    [StringLength(255)]
    public string PresidentEmail { get; set; }

    [Column("Treasurer Email")]
    [StringLength(255)]
    public string TreasurerEmail { get; set; }

    [Column("Projected Active Members")]
    public int? ProjectedActiveMembers { get; set; }

    [Required]
    [StringLength(255)]
    public string Decision { get; set; }

    [StringLength(255)]
    public string Notes { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime Timestamp { get; set; }
}