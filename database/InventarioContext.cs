using Microsoft.EntityFrameworkCore;

namespace database
{
    public class InventarioContext :DbContext
    {
        public InventarioContext(DbContextOptions<InventarioContext> options):base(options){ }

        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Almacen> Almacens { get; set; }


    }
}