using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class CategoriesClubMembership
    {
        public string FiscalYear { get; set; }
        public string Category { get; set; }
        public string ActiveMembers { get; set; }
    }
}
