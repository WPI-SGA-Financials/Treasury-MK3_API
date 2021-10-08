using System;
using System.Collections.Generic;

#nullable disable

namespace Treasury.Domain.Model
{
    public partial class OrganizationComment
    {
        public int Id { get; set; }
        public string NameOfClub { get; set; }
        public DateTime CommentDate { get; set; }
        public string Comment { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Organization NameOfClubNavigation { get; set; }
    }
}
