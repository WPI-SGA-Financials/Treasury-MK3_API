using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Treasury.Models
{
    [Table("Budget By FY")]
    public class Budget
    {
        [Column("Budget ID")] 
        public int ID { get; set; }
        
        [Column("Name of Club")]
        public string NameOfClub { get; set; }

        [Column("Fiscal Year")]
        public string FiscalYear { get; set; }

        [Column("Num Of Items")]
        public int NumOfItems { get; set; }

        [Column("Amount Requested")]
        public decimal AmountRequested { get; set; }

        [Column("Amount Proposed")]
        public decimal AmountProposed { get; set; }

        [Column("Approved Appeal")]
        public decimal ApprovedAppeal { get; set; }

        [Column("Amount Approved")]
        public decimal AmountApproved { get; set; }

        [Column("Amount Spent")]
        public decimal AmountSpent { get; set; }
    }
}
