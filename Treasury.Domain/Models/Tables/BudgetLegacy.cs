using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("BudgetLegacy")]
    [Index(nameof(BId), Name = "BudgetLegacy_Budget_ID_fk")]
    public partial class BudgetLegacy
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("B_ID")]
        public int BId { get; set; }

        [Column("Amount Requested")]
        public decimal AmountRequested { get; set; }

        [Column("Amount Proposed")]
        public decimal AmountProposed { get; set; }

        [Column(TypeName = "bit(1)")]
        public bool Appealed { get; set; }

        [Column("Appeal Amount")]
        public decimal AppealAmount { get; set; }

        [Required]
        [Column("Appeal Decision")]
        [StringLength(20)]
        public string AppealDecision { get; set; }

        [Column("Approved Appeal")]
        public decimal ApprovedAppeal { get; set; }

        [Column("Amount Spent")]
        public decimal AmountSpent { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(BId))]
        [InverseProperty(nameof(Budget.BudgetLegacies))]
        public virtual Budget BIdNavigation { get; set; }
    }
}