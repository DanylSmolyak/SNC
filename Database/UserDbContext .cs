
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    using System.Data.Entity;

    public class Context : DbContext
    {
        public Context() : base("DefaultConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<Context>());
        }


        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.ID);
        }
    

    //метод для переназначения UPDATE


    }

}
