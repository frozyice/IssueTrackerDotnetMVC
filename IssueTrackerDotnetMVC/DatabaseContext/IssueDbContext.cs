using IssueTrackerDotnetMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace IssueTrackerDotnetMVC.DatabaseContext
{
    public class IssueDbContext : DbContext
    {
        public IssueDbContext() : base("IssueDb")
        {
            //Database.SetInitializer(new IssueDbInitializer());
        }
        public DbSet<Issue> Issues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}