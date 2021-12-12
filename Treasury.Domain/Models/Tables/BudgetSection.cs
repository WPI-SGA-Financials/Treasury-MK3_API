using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("BudgetSection")]
    [Index(nameof(BId), Name = "BudgetSection_Budget_ID_fk")]
    public partial class BudgetSection
    {
        public BudgetSection()
        {
            BudgetLineItems = new HashSet<BudgetLineItem>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("B_ID")]
        public int BId { get; set; }

        [Required]
        [Column("Section_Name")]
        [StringLength(255)]
        public string SectionName { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(BId))]
        [InverseProperty(nameof(Budget.Sections))]
        public virtual Budget BIdNavigation { get; set; }

        [InverseProperty(nameof(BudgetLineItem.Bs))]
        public virtual ICollection<BudgetLineItem> BudgetLineItems { get; set; }
    }
}