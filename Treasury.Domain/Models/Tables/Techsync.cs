using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("Techsync Names")]
    public partial class TechsyncName
    {
        [Key]
        [Column("Name of Club")]
        public string NameOfClub { get; set; }

        [Column("Techsync Name")]
        [StringLength(255)]
        public string TechsyncName1 { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(NameOfClub))]
        [InverseProperty(nameof(Tables.Organization.TechsyncInfo))]
        public virtual Organization Organization { get; set; }
    }
}