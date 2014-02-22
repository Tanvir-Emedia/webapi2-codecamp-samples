using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sessions.Data.EF
{
    public class CodeCampContext: DbContext
    {
        public CodeCampContext()
            : base("name=CodeCampSessions")
        {
        }
        static CodeCampContext()
        {
             var type =  typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }
        public virtual DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SessionConfiguration());
        }
    }
}
