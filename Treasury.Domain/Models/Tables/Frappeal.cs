using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("FRAppeals")]
    [Index(nameof(FrId), Name = "FRAppeals_FR_ID_uindex", IsUnique = true)]
    public partial class Frappeal
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("FR_ID")]
        public int FrId { get; set; }
        [Required]
        [Column("New Dot Number")]
        [StringLength(6)]
        public string NewDotNumber { get; set; }
        [Column("Appeal Date", TypeName = "date")]
        public DateTime AppealDate { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Column("Appeal Amount")]
        public decimal AppealAmount { get; set; }
        [Required]
        [StringLength(20)]
        public string Decision { get; set; }
        [Column("Approved Appeal")]
        public decimal ApprovedAppeal { get; set; }
        [StringLength(255)]
        public string Notes { get; set; }
        [Column("Minutes Link")]
        [StringLength(255)]
        public string MinutesLink { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(FrId))]
        [InverseProperty(nameof(FundingRequest.Frappeal))]
        public virtual FundingRequest Fr { get; set; }
    }
}
