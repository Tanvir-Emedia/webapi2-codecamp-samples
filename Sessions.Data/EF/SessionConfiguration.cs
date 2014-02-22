using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Sessions.Data.EF
{
    public class SessionConfiguration: EntityTypeConfiguration<Session>
    {
        public SessionConfiguration()
        {
            HasKey(s => s.ID)
             .Property(s => s.ID)
             .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            Property(s => s.Title).IsRequired();
            Property(s => s.Presenter).IsRequired();
        }
    }
}
