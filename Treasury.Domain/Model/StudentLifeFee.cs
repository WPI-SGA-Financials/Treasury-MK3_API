using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class StudentLifeFee
    {
        public int Id { get; set; }
        public string FiscalYear { get; set; }
        public decimal SlfAmount { get; set; }
        public int? FallStudentAmount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
