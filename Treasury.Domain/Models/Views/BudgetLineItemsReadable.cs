using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Views
{
    [Keyless]
    public partial class BudgetLineItemsReadable
    {
        [Required]
        [Column("Name of Club")]
        [StringLength(255)]
        public string NameOfClub { get; set; }
        [Required]
        [Column("Fiscal Year")]
        [StringLength(6)]
        public string FiscalYear { get; set; }
        [Required]
        [Column("Section Name")]
        [StringLength(255)]
        public string SectionName { get; set; }
        [Required]
        [Column("Line Item Name")]
        [StringLength(255)]
        public string LineItemName { get; set; }
        [Column("Amount Requested")]
        public decimal AmountRequested { get; set; }
        [Column("Amount Proposed")]
        public decimal AmountProposed { get; set; }
        [Column("Approved Appeal")]
        public decimal ApprovedAppeal { get; set; }
        [Column("Amount Spent")]
        public decimal AmountSpent { get; set; }
    }
}
