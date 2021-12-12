using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Views
{
    [Keyless]
    public partial class TotalLifeFeeBudget
    {
        [Required]
        [Column("Fiscal Year")]
        [StringLength(6)]
        public string FiscalYear { get; set; }

        public decimal? Total { get; set; }
    }
}