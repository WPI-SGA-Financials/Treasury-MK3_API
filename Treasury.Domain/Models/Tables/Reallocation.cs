using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Index(nameof(NameOfClub), Name = "orgRealName_idx")]
    public partial class Reallocation
    {
        public Reallocation()
        {
            ReallocMinutes = new HashSet<ReallocMinute>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("Name of Club")]
        public string NameOfClub { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

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

        [Column("Allocated From")]
        [StringLength(255)]
        public string AllocatedFrom { get; set; }

        [Column("Allocated To")]
        [StringLength(255)]
        public string AllocatedTo { get; set; }

        [Column("Allocation Amount")]
        public decimal AllocationAmount { get; set; }

        [Required]
        [StringLength(255)]
        public string Decision { get; set; }

        [Column("Amount Approved")]
        public decimal AmountApproved { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(NameOfClub))]
        [InverseProperty(nameof(Organization.Reallocations))]
        public virtual Organization NameOfClubNavigation { get; set; }

        [InverseProperty(nameof(ReallocMinute.Realloc))]
        public virtual ICollection<ReallocMinute> ReallocMinutes { get; set; }
    }
}