using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Views
{
    [Keyless]
    public partial class CategoriesClubMembership
    {
        [Required]
        [Column("Fiscal Year")]
        [StringLength(255)]
        public string FiscalYear { get; set; }
        [Required]
        [StringLength(255)]
        public string Category { get; set; }
        [Required]
        [Column("Active Members")]
        [StringLength(255)]
        public string ActiveMembers { get; set; }
    }
}
