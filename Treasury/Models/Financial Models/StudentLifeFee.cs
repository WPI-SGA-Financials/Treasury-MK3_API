using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Treasury.Models
{
    [Table("student life fee")]
    public class StudentLifeFee
    {
        public int ID { get; set; }
        [Column("Fiscal Year")]
        public string FiscalYear { get; set; }
        [Column("SLF Amount")]
        public decimal SlfAmount { get; set; }
        [Column("Fall Student Amount")]
        public int? FallStudentAmt { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
