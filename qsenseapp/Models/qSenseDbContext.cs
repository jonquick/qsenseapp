using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace qsenseapp.Models
{
    public class qSenseDbContext : DbContext
    {
        public DbSet<BusinessPartner> BusinessPartners { get; set; }
        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<qsenseapp.Models.JobExpense> JobExpenses { get; set; }

    }
}