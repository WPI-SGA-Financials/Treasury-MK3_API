using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Treasury.Models.Financial_Models
{
    [Table("Reallocations")]
    public class Reallocation
    {
        public int ID { get; set; }

        [Column("Name of Club")]
        public string NameOfClub { get; set; }

        public string Description { get; set; }

        [Column("Hearing Date")]
        public DateTime HearingDate { get; set; }

        [Column("Fiscal Year")]
        public string FiscalYear { get; set; }

        [Column("Dot Number")]
        public string DotNumber { get; set; }

        [Column("Allocated From")]
        public string AllocatedFrom { get; set; }

        [Column("Allocated To")]
        public string AllocatedTo { get; set; }

        [Column("Allocation Amount")]
        public decimal AllocationAmount { get; set; }

        public string Decision { get; set; }

        [Column("Amount Approved")]
        public decimal AmountApproved { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
