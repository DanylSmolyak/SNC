using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Context : DbContext
    {
        public Context() : base("Server=DESKTOP-UEUPIBL\\\\MSSQLSERVER1;Database=TestSNC;Trusted_Connection=True;MultipleActiveResultSets=True; TrustServerCertificate=True")
        {
        }


        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.ID);
        }

    }
}
