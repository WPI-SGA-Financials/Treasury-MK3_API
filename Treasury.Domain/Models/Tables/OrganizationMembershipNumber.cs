using System;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    public partial class OrganizationMembershipNumber
    {
        public string NameOfOrganization { get; set; }
        public string FiscalYear { get; set; }
        public string ActiveMembers { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Organization NameOfOrganizationNavigation { get; set; }
    }
}
