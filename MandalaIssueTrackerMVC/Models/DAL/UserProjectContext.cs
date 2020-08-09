using Microsoft.EntityFrameworkCore;
using MandalaIssueTrackerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Text;

namespace MandalaIssueTrackerMVC.Models.DAL
{
    public class UserProjectContext : DbContext
    {

        public UserProjectContext() : base() { }

        public UserProjectContext(DbContextOptions<UserProjectContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<PluralizingTableNameConvention>();
            modelBuilder.Entity<ProjectUser>()
                .HasKey(o => new { o.ProjId, o.UserId });
            modelBuilder.Entity<User>().HasData(
                        new User
                        {
                            UserId = 1,
                            Name = "Alex Campbell",
                            Jobname = "Project Leader",
                            Hashpass = Encoding.ASCII.GetBytes("Hieracon"),
                            Salt = Encoding.ASCII.GetBytes("2405"),
                            Username = "ACampb2405",
                            IsAdmin = true
                        }
                );
            modelBuilder.Entity<Project>().HasData(
                        new Project
                        {
                            ProjId = 1,
                            Name = "Project 1",
                            Description = "This is the first test project",
                            StatusId = 0,
                            ManagerId = 1
                        }
                );
            modelBuilder.Entity<ProjectUser>().HasData(
                        new ProjectUser
                        {
                            ProjId = 1,
                            UserId = 1
                        }
                );
        }

    }
}
