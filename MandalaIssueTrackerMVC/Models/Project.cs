using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandalaIssueTrackerMVC.Models
{
    [Table("projects")]
    public partial class Project
    {
        public Project()
        {
            ProjectUsers = new HashSet<ProjectUser>();
        }

        [Key]
        [Column("proj_id")]
        public int ProjId { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("description")]
        [StringLength(200)]
        public string Description { get; set; }
        [Column("status_id")]
        public int StatusId { get; set; }
        [Column("manager_id")]
        public int ManagerId { get; set; }

        [InverseProperty("Proj")]
        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
    }
}
