using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("Mandatory Transfers")]
    [Index(nameof(ParentOrganization), Name = "Mandatory Transfers_Organizations_Name of Club_fk")]
    public partial class MandatoryTransfer
    {
        public MandatoryTransfer()
        {
            MtlineItems = new HashSet<MtlineItem>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("Parent Organization")]
        public string ParentOrganization { get; set; }
        [Required]
        [Column("Fund Name")]
        [StringLength(255)]
        public string FundName { get; set; }
        [Required]
        [Column("Fiscal Year")]
        [StringLength(255)]
        public string FiscalYear { get; set; }
        [Required]
        [StringLength(255)]
        public string Worktag { get; set; }
        [Column("Amount Requested")]
        public decimal AmountRequested { get; set; }
        [Column("Amount Proposed")]
        public decimal AmountProposed { get; set; }
        [Column("Amount Approved")]
        public decimal AmountApproved { get; set; }
        [StringLength(255)]
        public string Notes { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(ParentOrganization))]
        [InverseProperty(nameof(Organization.MandatoryTransfers))]
        public virtual Organization ParentOrganizationNavigation { get; set; }
        [InverseProperty(nameof(MtlineItem.Mt))]
        public virtual ICollection<MtlineItem> MtlineItems { get; set; }
    }
}
