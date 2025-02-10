using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WcfService.Models;

namespace WcfService.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(): base("Server=DESKTOP-UEUPIBL\\MSSQLSERVER1;Database=TestSNC;User Id=sa;Password=123123;MultipleActiveResultSets=True;TrustServerCertificate=True;")

        { }


        public DbSet<User> Users { get; set; }
    }
}