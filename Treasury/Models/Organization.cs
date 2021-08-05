using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treasury.Models
{
    public class Organization
    {
        [Key]
        [Column("Name of Club")]
        public string Name { get; set; }
        public string Classification { get; set; }
        [Column("Type of Club")]
        public string TypeOfClub { get; set; }
        [Column("Account Number")]
        public string AccountNumber { get; set; }
        [Column("Acronym 1")]
        public string Acronym { get; set; }
        [Column("Inactive?")]
        public bool Inactive { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
