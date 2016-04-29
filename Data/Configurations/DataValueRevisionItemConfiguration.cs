using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Model;

namespace Data.Configurations
{
    public class DataValueRevisionConfiguration : EntityTypeConfiguration<DataValueRevisionItem>
    {
        public DataValueRevisionConfiguration()
        {
            this.Property(c => c.id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }
}
