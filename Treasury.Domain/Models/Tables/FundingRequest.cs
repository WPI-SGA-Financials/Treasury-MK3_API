using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("Funding Requests")]
    [Index(nameof(NameOfClub), Name = "Funding Requests_Organizations_Name of Club_fk")]
    public partial class FundingRequest
    {
        public FundingRequest()
        {
            Frsupplementals = new HashSet<Frsupplemental>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("Name of Club")]
        public string NameOfClub { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Column("Funding Date", TypeName = "date")]
        public DateTime FundingDate { get; set; }
        [Column("Fiscal Year")]
        [StringLength(6)]
        public string FiscalYear { get; set; }
        [Column("Date of Event", TypeName = "date")]
        public DateTime? DateOfEvent { get; set; }
        [Required]
        [Column("Dot Number")]
        [StringLength(6)]
        public string DotNumber { get; set; }
        [Column("Amount Requested")]
        public decimal AmountRequested { get; set; }
        [Required]
        [StringLength(20)]
        public string Decision { get; set; }
        [Column("Amount Approved")]
        public decimal AmountApproved { get; set; }
        [StringLength(512)]
        public string Notes { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(NameOfClub))]
        [InverseProperty(nameof(Organization.FundingRequests))]
        public virtual Organization NameOfClubNavigation { get; set; }
        [InverseProperty("Fr")]
        public virtual Frappeal Frappeal { get; set; }
        [InverseProperty("Fr")]
        public virtual Frminute Frminute { get; set; }
        [InverseProperty("Fr")]
        public virtual FrreportForm FrreportForm { get; set; }
        [InverseProperty("Fr")]
        public virtual FrworkdayIdt FrworkdayIdt { get; set; }
        [InverseProperty(nameof(Frsupplemental.Fr))]
        public virtual ICollection<Frsupplemental> Frsupplementals { get; set; }
    }
}
