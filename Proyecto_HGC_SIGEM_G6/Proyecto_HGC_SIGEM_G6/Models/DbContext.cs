using Microsoft.EntityFrameworkCore;

namespace Proyecto_HGC_SIGEM_G6.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Orden> Orden { get; set; }
        public DbSet<Widget> Widget { get; set; }
        public DbSet<ConfiguracionUsuario> ConfiguracionUsuario { get; set; }
    }
}
