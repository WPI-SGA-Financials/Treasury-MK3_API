using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Index(nameof(ReclassId), Name = "ReclassMinutes_Reclassifications_ID_fk")]
    public partial class ReclassMinute
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("Agenda Number")]
        [StringLength(9)]
        public string AgendaNumber { get; set; }
        [Column("Reclass_ID")]
        public int ReclassId { get; set; }
        [Required]
        [Column("Minutes Link")]
        [StringLength(255)]
        public string MinutesLink { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime? Timestamp { get; set; }

        [ForeignKey(nameof(ReclassId))]
        [InverseProperty(nameof(Reclassification.ReclassMinutes))]
        public virtual Reclassification Reclass { get; set; }
    }
}
