using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Treasury.Domain.Models.Tables
{
    [Table("Organization Comments")]
    [Index(nameof(NameOfClub), Name = "orgCommentName_idx")]
    public partial class OrganizationComment
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("Name of Club")]
        public string NameOfClub { get; set; }

        [Column("Comment Date", TypeName = "date")]
        public DateTime CommentDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Comment { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(NameOfClub))]
        [InverseProperty(nameof(Organization.OrganizationComments))]
        public virtual Organization NameOfClubNavigation { get; set; }
    }
}