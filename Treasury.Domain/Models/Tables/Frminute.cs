﻿using System;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    public partial class Frminute
    {
        public int Id { get; set; }
        public string AgendaNumber { get; set; }
        public int FrId { get; set; }
        public string MinutesLink { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual FundingRequest Fr { get; set; }
    }
}