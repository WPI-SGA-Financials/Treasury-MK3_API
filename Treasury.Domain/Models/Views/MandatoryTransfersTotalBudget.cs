using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Views
{
    [Keyless]
    public partial class MandatoryTransfersTotalBudget
    {
        [Required]
        [Column("Fiscal Year")]
        [StringLength(255)]
        public string FiscalYear { get; set; }

        [Required]
        [Column("Parent Organization")]
        [StringLength(255)]
        public string ParentOrganization { get; set; }

        [Required]
        [Column("Fund Name")]
        [StringLength(255)]
        public string FundName { get; set; }

        [Column("Total Budget")]
        public decimal TotalBudget { get; set; }
    }
}