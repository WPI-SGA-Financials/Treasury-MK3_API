﻿using System;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    public partial class OrganizationsContactInfo
    {
        public string NameOfClub { get; set; }
        public string PresidentEmail { get; set; }
        public string TreasurerEmail { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Organization NameOfClubNavigation { get; set; }
    }
}