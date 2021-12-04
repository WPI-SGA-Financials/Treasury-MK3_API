using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("FRSupplemental")]
    [Index(nameof(FrId), Name = "FRSupplemental_Funding Requests_ID_fk")]
    public partial class Frsupplemental
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("FR_ID")]
        public int FrId { get; set; }
        [Required]
        [Column("Item Type")]
        [StringLength(100)]
        public string ItemType { get; set; }
        [Column("Other Type")]
        [StringLength(100)]
        public string OtherType { get; set; }
        [Column("Amount Requested")]
        public decimal AmountRequested { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool? Amended { get; set; }
        [Column("Amended Amount")]
        public decimal? AmendedAmount { get; set; }
        [StringLength(255)]
        public string Notes { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime? Timestamp { get; set; }

        [ForeignKey(nameof(FrId))]
        [InverseProperty(nameof(FundingRequest.Frsupplementals))]
        public virtual FundingRequest Fr { get; set; }
    }
}
