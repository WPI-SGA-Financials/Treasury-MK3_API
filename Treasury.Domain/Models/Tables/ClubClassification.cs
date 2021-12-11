using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("Club Classifications")]
    public partial class ClubClassification
    {
        [Key]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Category { get; set; }

        [ForeignKey(nameof(Name))]
        [InverseProperty(nameof(Tables.Organization.ClubCategory))]
        public virtual Organization Organization { get; set; }
    }
}