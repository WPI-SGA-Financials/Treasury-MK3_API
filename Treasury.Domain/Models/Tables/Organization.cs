using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Treasury.Domain.Models.Tables;

public partial class Organization
{
    public Organization()
    {
        Budgets = new HashSet<Budget>();
        FundingRequests = new HashSet<FundingRequest>();
        MandatoryTransfers = new HashSet<MandatoryTransfer>();
        OrganizationComments = new HashSet<OrganizationComment>();
        OrganizationMembershipNumbers = new HashSet<OrganizationMembershipNumber>();
        Reallocations = new HashSet<Reallocation>();
        Reclassifications = new HashSet<Reclassification>();
    }

    [Key]
    [Column("Name of Club")]
    public string NameOfClub { get; set; }

    [StringLength(100)]
    public string Classification { get; set; }

    [Column("Type of Club")]
    [StringLength(100)]
    public string TypeOfClub { get; set; }

    [Column("Account Number")]
    [StringLength(8)]
    public string AccountNumber { get; set; }

    [Column("Acronym 1")]
    [StringLength(50)]
    public string Acronym1 { get; set; }

    [Column("Inactive?", TypeName = "bit(1)")]
    public bool Inactive { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime Timestamp { get; set; }

    [InverseProperty(nameof(ClubClassification.Organization))]
    public virtual ClubClassification ClubCategory { get; set; }

    [InverseProperty(nameof(OrganizationContactInfo.Organization))]
    public virtual OrganizationContactInfo ContactInfo { get; set; }

    [InverseProperty(nameof(Techsync.Organization))]
    public virtual Techsync TechsyncInfo { get; set; }

    [InverseProperty(nameof(Budget.Organization))]
    public virtual ICollection<Budget> Budgets { get; set; }

    [InverseProperty(nameof(FundingRequest.Organization))]
    public virtual ICollection<FundingRequest> FundingRequests { get; set; }

    [InverseProperty(nameof(MandatoryTransfer.Organization))]
    public virtual ICollection<MandatoryTransfer> MandatoryTransfers { get; set; }

    [InverseProperty(nameof(OrganizationComment.Organization))]
    public virtual ICollection<OrganizationComment> OrganizationComments { get; set; }

    [InverseProperty(nameof(OrganizationMembershipNumber.Organization))]
    public virtual ICollection<OrganizationMembershipNumber> OrganizationMembershipNumbers { get; set; }

    [InverseProperty(nameof(Reallocation.Organization))]
    public virtual ICollection<Reallocation> Reallocations { get; set; }

    [InverseProperty(nameof(Reclassification.Organization))]
    public virtual ICollection<Reclassification> Reclassifications { get; set; }
}