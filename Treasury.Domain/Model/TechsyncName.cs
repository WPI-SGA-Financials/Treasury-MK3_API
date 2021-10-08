using System;
using System.Text.Json.Serialization;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class TechsyncName
    {
        [JsonIgnore]
        public string NameOfClub { get; set; }
        public string TechsyncName1 { get; set; }
        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public virtual Organization NameOfClubNavigation { get; set; }
    }
}
