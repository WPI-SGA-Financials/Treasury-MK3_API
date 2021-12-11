using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("Organizations Contact Info")]
    public partial class OrganizationsContactInfo
    {
        [Key]
        [Column("Name of Club")]
        public string NameOfClub { get; set; }

        [Column("President Email")]
        [StringLength(255)]
        public string PresidentEmail { get; set; }

        [Column("Treasurer Email")]
        [StringLength(255)]
        public string TreasurerEmail { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(NameOfClub))]
        [InverseProperty(nameof(Tables.Organization.ContactInfo))]
        public virtual Organization Organization { get; set; }
    }
}