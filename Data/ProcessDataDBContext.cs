using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Model;
using Data.Configurations;

namespace Data
{
    public partial class ProcessDataDBContext : DbContext
    {
        public ProcessDataDBContext()
            : base("ProcessCardDataSystem")
        { 
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<DataValue> DataValues { get; set; }
        public DbSet<Revision> Revisions { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateData> TemplateData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DocumentConfiguration());
            modelBuilder.Configurations.Add(new TemplateConfiguration());
            modelBuilder.Configurations.Add(new TemplateDataConfiguration());
            modelBuilder.Configurations.Add(new DataValueConfiguration());
            modelBuilder.Configurations.Add(new RevisionConfiguration());
        }
    }
}
