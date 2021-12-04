using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("Organization Membership Numbers")]
    public partial class OrganizationMembershipNumber
    {
        [Key]
        [Column("Name of Organization")]
        public string NameOfOrganization { get; set; }
        [Key]
        [Column("Fiscal Year")]
        public string FiscalYear { get; set; }
        [Required]
        [Column("Active Members")]
        [StringLength(255)]
        public string ActiveMembers { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(NameOfOrganization))]
        [InverseProperty(nameof(Organization.OrganizationMembershipNumbers))]
        public virtual Organization NameOfOrganizationNavigation { get; set; }
    }
}
