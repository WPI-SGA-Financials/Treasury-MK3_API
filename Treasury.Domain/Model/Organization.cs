using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
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

        public string NameOfClub { get; set; }
        public string Classification { get; set; }
        public string TypeOfClub { get; set; }
        public string AccountNumber { get; set; }
        public string Acronym1 { get; set; }
        public ulong Inactive { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual ClubClassification ClubClassification { get; set; }
        public virtual OrganizationsContactInfo OrganizationsContactInfo { get; set; }
        public virtual TechsyncName TechsyncName { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<FundingRequest> FundingRequests { get; set; }
        public virtual ICollection<MandatoryTransfer> MandatoryTransfers { get; set; }
        public virtual ICollection<OrganizationComment> OrganizationComments { get; set; }
        public virtual ICollection<OrganizationMembershipNumber> OrganizationMembershipNumbers { get; set; }
        public virtual ICollection<Reallocation> Reallocations { get; set; }
        public virtual ICollection<Reclassification> Reclassifications { get; set; }
    }
}
