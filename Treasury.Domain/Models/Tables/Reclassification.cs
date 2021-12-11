using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Index(nameof(NameOfClub), Name = "orgReclName_idx")]
    public partial class Reclassification
    {
        public Reclassification()
        {
            ReclassMinutes = new HashSet<ReclassMinute>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("Name of Club")]
        public string NameOfClub { get; set; }

        [Column("Hearing Date", TypeName = "date")]
        public DateTime HearingDate { get; set; }

        [Required]
        [Column("Fiscal Year")]
        [StringLength(255)]
        public string FiscalYear { get; set; }

        [Required]
        [Column("Dot Number")]
        [StringLength(255)]
        public string DotNumber { get; set; }

        [Required]
        [Column("Original Class")]
        [StringLength(255)]
        public string OriginalClass { get; set; }

        [Required]
        [Column("Requested Class")]
        [StringLength(255)]
        public string RequestedClass { get; set; }

        [Required]
        [StringLength(255)]
        public string Decision { get; set; }

        [Required]
        [Column("Approved Class")]
        [StringLength(255)]
        public string ApprovedClass { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(NameOfClub))]
        [InverseProperty(nameof(Organization.Reclassifications))]
        public virtual Organization NameOfClubNavigation { get; set; }

        [InverseProperty(nameof(ReclassMinute.Reclass))]
        public virtual ICollection<ReclassMinute> ReclassMinutes { get; set; }
    }
}