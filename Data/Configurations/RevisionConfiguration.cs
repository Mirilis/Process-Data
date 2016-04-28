using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Model;

namespace Data.Configurations
{
    public class RevisionConfiguration : EntityTypeConfiguration<Revision>
    {
        public RevisionConfiguration()
        {
            this.Property(c => c.id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }
}
