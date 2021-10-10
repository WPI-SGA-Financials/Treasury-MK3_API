using System;

#nullable disable

namespace Treasury.Domain.Models.Tables
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
