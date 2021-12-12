using Treasury.Domain.Models.Tables;

namespace Treasury.Domain.Models
{
    public interface IOrgBasedEntity
    {
        public Organization Organization { get; set; }
    }
}