using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Treasury.Models.Financial_Models.Budget_Models
{
    [Table("Budget By Section")]
    [Keyless]
    public class BudgetSection
    {
        [Column("Name of Club")]
        public string NameOfClub { get; set; }

        [Column("Fiscal Year")]
        public string FiscalYear { get; set; }

        [Column("Section Name")]
        public string SectionName { get; set; }

        [Column("Num Of Items")]
        public int NumOfItems { get; set; }

        [Column("Amount Requested")]
        public decimal AmountRequested { get; set; }

        [Column("Amount Proposed")]
        public decimal AmountProposed { get; set; }

        [Column("Approved Appeal")]
        public decimal ApprovedAppeal { get; set; }

        /*[Column("Amount Approved")]
        public decimal AmountApproved { get; set; }*/

        [Column("Amount Spent")]
        public decimal AmountSpent { get; set; }
    }
}
