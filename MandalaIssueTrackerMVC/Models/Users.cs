using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandalaIssueTrackerMVC.Models
{
    [Table("users")]
    public partial class Users
    {
        public Users()
        {
            ProjectUsers = new HashSet<ProjectUsers>();
        }

        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("jobname")]
        [StringLength(60)]
        public string Jobname { get; set; }
        [Required]
        [Column("hashpass")]
        public byte[] Hashpass { get; set; }
        [Required]
        [Column("salt")]
        [MaxLength(30)]
        public byte[] Salt { get; set; }
        [Required]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }
        [Column("is_admin")]
        public bool? IsAdmin { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<ProjectUsers> ProjectUsers { get; set; }
    }
}
