using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTicketingSystem.Models;
//using OnlineTicketingSystem.Migrations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace OnlineTicketingSystem.Data
{
    public class OnlineTicketingSystemContext : DbContext
    {
        public OnlineTicketingSystemContext() : base("OnlineTicketingSystem")
        {
            //Database.SetInitializer<OnlineTicketingSystemContext>(new CreateDatabaseIfNotExists<OnlineTicketingSystemContext>());
            //Database.SetInitializer<OnlineTicketingSystemContext>(new DropCreateDatabaseIfModelChanges<OnlineTicketingSystemContext>());
            //Database.SetInitializer<OnlineTicketingSystemContext>(new DropCreateDatabaseAlways<OnlineTicketingSystemContext>());
            
            //Database.SetInitializer<OnlineTicketingSystemContext>(new DBInitializer());

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<OnlineTicketingSystemContext,Configuration>());
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }        
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //base.OnModelCreating(modelBuilder);
        }
    }
}