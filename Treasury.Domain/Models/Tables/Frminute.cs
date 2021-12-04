using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("FRMinutes")]
    [Index(nameof(FrId), Name = "FR_ID_UNIQUE", IsUnique = true)]
    public partial class Frminute
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("Agenda Number")]
        [StringLength(9)]
        public string AgendaNumber { get; set; }
        [Column("FR_ID")]
        public int FrId { get; set; }
        [Column("Minutes Link")]
        [StringLength(255)]
        public string MinutesLink { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(FrId))]
        [InverseProperty(nameof(FundingRequest.Frminute))]
        public virtual FundingRequest Fr { get; set; }
    }
}
