using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    public partial class Budget
    {
        public Budget()
        {
            BudgetLegacies = new HashSet<BudgetLegacy>();
            BudgetSections = new HashSet<BudgetSection>();
        }

        public int Id { get; set; }
        public string NameOfClub { get; set; }
        public string FiscalYear { get; set; }
        public string Notes { get; set; }
        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public virtual Organization NameOfClubNavigation { get; set; }
        public virtual ICollection<BudgetLegacy> BudgetLegacies { get; set; }
        public virtual ICollection<BudgetSection> BudgetSections { get; set; }
    }
}
