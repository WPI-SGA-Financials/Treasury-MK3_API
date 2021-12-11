using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("MTLineItems")]
    [Index(nameof(MtId), Name = "MT_LineItems_MT_ID_index")]
    public partial class MtlineItem
    {
        public MtlineItem()
        {
            OperatingExpenses = new HashSet<OperatingExpense>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("MT_ID")]
        public int MtId { get; set; }

        [Required]
        [Column("Line Item")]
        [StringLength(255)]
        public string LineItem { get; set; }

        public decimal Amount { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(MtId))]
        [InverseProperty(nameof(MandatoryTransfer.MtlineItems))]
        public virtual MandatoryTransfer Mt { get; set; }

        [InverseProperty(nameof(OperatingExpense.Mtli))]
        public virtual ICollection<OperatingExpense> OperatingExpenses { get; set; }
    }
}