using System;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    public partial class ReallocMinute
    {
        public int Id { get; set; }
        public string AgendaNumber { get; set; }
        public int ReallocId { get; set; }
        public string MinutesLink { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Reallocation Realloc { get; set; }
    }
}
