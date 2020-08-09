using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MandalaIssueTrackerMVC.Models
{
    public partial class IssueTrackerContext : DbContext
    {
        public IssueTrackerContext()
        {
        }

        public IssueTrackerContext(DbContextOptions<IssueTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProjectUser> ProjectUsers { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<User> Users { get; set; }

        /* TEMPORARILY REMOVED THIS TO TEST.
         * 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\SQLExpress;Database=IssueTracker;Trusted_Connection=True;");
            }
        }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectUser>(entity =>
            {
                entity.HasKey(e => new { e.ProjId, e.UserId });

                entity.HasOne(d => d.Proj)
                    .WithMany(p => p.ProjectUsers)
                    .HasForeignKey(d => d.ProjId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_project_users_projects");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_project_users_users");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.ProjId).ValueGeneratedNever();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Username).IsUnicode(false);
            });

            //This should add some basic data into the place on migration
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


            OnModelCreatingPartial(modelBuilder);
        }




        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
