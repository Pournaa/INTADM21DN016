using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ProjectBusss.Models
{
    public class ProjectBusContext : DbContext
    {
        public ProjectBusContext() : base("ProjectBusReContext")
        {

        }

        public DbSet<Bus> Bus { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Payment> Payment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}