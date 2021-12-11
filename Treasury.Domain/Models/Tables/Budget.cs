using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("Budget")]
    [Index(nameof(NameOfClub), Name = "Name of Club")]
    public partial class Budget
    {
        public Budget()
        {
            BudgetLegacies = new HashSet<BudgetLegacy>();
            BudgetSections = new HashSet<BudgetSection>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("Name of Club")]
        public string NameOfClub { get; set; }

        [Required]
        [Column("Fiscal Year")]
        [StringLength(6)]
        public string FiscalYear { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(NameOfClub))]
        [InverseProperty(nameof(Tables.Organization.Budgets))]
        public virtual Organization Organization { get; set; }

        [InverseProperty(nameof(BudgetLegacy.BIdNavigation))]
        public virtual ICollection<BudgetLegacy> BudgetLegacies { get; set; }

        [InverseProperty(nameof(BudgetSection.BIdNavigation))]
        public virtual ICollection<BudgetSection> BudgetSections { get; set; }
    }
}