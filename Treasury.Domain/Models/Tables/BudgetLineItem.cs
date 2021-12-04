using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("BudgetLineItem")]
    [Index(nameof(BsId), Name = "BudgetLineItem_BudgetSection_ID_fk")]
    public partial class BudgetLineItem
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("BS_ID")]
        public int BsId { get; set; }
        [Required]
        [Column("Line_Item_Name")]
        [StringLength(255)]
        public string LineItemName { get; set; }
        [Column("Amount_Request")]
        public decimal AmountRequest { get; set; }
        [Column("Amount_Proposed")]
        public decimal AmountProposed { get; set; }
        [Column(TypeName = "bit(1)")]
        public bool Appealed { get; set; }
        [Column("Appeal_Amount")]
        public decimal AppealAmount { get; set; }
        [Required]
        [Column("Appeal_Decision")]
        [StringLength(20)]
        public string AppealDecision { get; set; }
        [Column("Approved_Appeal")]
        public decimal ApprovedAppeal { get; set; }
        [Required]
        [StringLength(255)]
        public string Notes { get; set; }
        [Column("Amount_Spent")]
        public decimal AmountSpent { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(BsId))]
        [InverseProperty(nameof(BudgetSection.BudgetLineItems))]
        public virtual BudgetSection Bs { get; set; }
    }
}
