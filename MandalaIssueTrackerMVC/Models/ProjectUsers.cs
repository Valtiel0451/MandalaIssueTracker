using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandalaIssueTrackerMVC.Models
{
    [Table("project_users")]
    public partial class ProjectUsers
    {
        [Key]
        [Column("proj_id")]
        public int ProjId { get; set; }
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey(nameof(ProjId))]
        [InverseProperty(nameof(Project.ProjectUsers))]
        public virtual Project Proj { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.ProjectUsers))]
        public virtual Users User { get; set; }
    }
}
