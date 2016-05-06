using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using Model;


namespace Data.Configurations
{
    public class DataValueConfiguration : EntityTypeConfiguration<DataValue>
    {
        public DataValueConfiguration()
        {
            this.HasMany(DataValue.DataValuesRevisionItemsAccessor);
            this.Property(c => c.id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
         
            
        }

        
    }
}
