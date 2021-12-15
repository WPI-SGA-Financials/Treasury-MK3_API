using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables;

[Index(nameof(ReallocId), Name = "ReallocMinutes_Reallocations_ID_fk")]
public partial class ReallocMinute
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [Column("Agenda Number")]
    [StringLength(9)]
    public string AgendaNumber { get; set; }

    [Column("Realloc_ID")]
    public int ReallocId { get; set; }

    [Required]
    [Column("Minutes Link")]
    [StringLength(255)]
    public string MinutesLink { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime Timestamp { get; set; }

    [ForeignKey(nameof(ReallocId))]
    [InverseProperty(nameof(Reallocation.ReallocMinutes))]
    public virtual Reallocation Realloc { get; set; }
}