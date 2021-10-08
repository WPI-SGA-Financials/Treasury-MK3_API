using System.Text.Json.Serialization;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class ClubClassification
    {
        public string Name { get; set; }
        public string Category { get; set; }

        [JsonIgnore]
        public virtual Organization NameNavigation { get; set; }
    }
}
