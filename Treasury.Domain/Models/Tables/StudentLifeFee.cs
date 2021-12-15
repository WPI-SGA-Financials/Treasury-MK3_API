using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Treasury.Domain.Models.Tables;

[Table("Student Life Fee")]
public partial class StudentLifeFee
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [Column("Fiscal Year")]
    [StringLength(6)]
    public string FiscalYear { get; set; }

    [Column("SLF Amount")]
    public decimal SlfAmount { get; set; }

    [Column("Fall Student Amount")]
    public int? FallStudentAmount { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime Timestamp { get; set; }
}