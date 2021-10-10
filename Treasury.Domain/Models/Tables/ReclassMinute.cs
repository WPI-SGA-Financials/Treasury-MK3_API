using System;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    public partial class ReclassMinute
    {
        public int Id { get; set; }
        public string AgendaNumber { get; set; }
        public int ReclassId { get; set; }
        public string MinutesLink { get; set; }
        public DateTime? Timestamp { get; set; }

        public virtual Reclassification Reclass { get; set; }
    }
}
