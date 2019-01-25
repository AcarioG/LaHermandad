using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace La_Hermandad.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(): base("Conexion")
        {
            Database.SetInitializer<ApplicationDbContext>(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }


        public DbSet<Comics> Comics { get; set; }
        public DbSet<PaginasComics> Paginas { get; set; }

    }
}