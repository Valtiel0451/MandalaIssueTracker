using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandalaIssueTrackerMVC.Models.DAL
{
    public static class SeedData 
    {
        public static void Initialize(IServiceProvider serviceProvider) 
        {
            using (var context = new IssueTrackerContext()) 
            {
                if (context.ProjectUsers.Any()) {
                    context.Users.AddRange(
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
                    context.Projects.AddRange(
                        new Project
                        {
                            ProjId = 1,
                            Name = "Project 1",
                            Description = "This is the first test project",
                            StatusId = 0,
                            ManagerId = 1
                        }
                    );
                    context.ProjectUsers.AddRange(
                        new ProjectUser
                        {
                            ProjId = 1,
                            UserId = 1
                        }
                    );
                    context.SaveChanges();

                }
            }
        }
    }
}
