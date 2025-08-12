using Microsoft.EntityFrameworkCore;
using ModelHelpes.Models;             
using ModelHelpes.Models.Widgets;      

namespace Proyecto_HGC_SIGEM_G6.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Widget> Widgets { get; set; }
        public DbSet<ConfiguracionUsuario> ConfiguracionUsuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Usuario>(e =>
            {
                e.ToTable("Usuarios");
                e.HasKey(u => u.IdUsuario);
                e.Property(u => u.Nombre).HasMaxLength(100).IsRequired();
                e.Property(u => u.Correo).HasMaxLength(100).IsRequired();
                e.Property(u => u.Rol).HasMaxLength(50).IsRequired();
                e.Property(u => u.ContrasenaHash)
                    .HasColumnName("ContraseñaHash")
                    .HasMaxLength(255)
                    .IsRequired();
            });

            
            modelBuilder.Entity<Widget>(e =>
            {
                e.ToTable("Widgets");
                e.HasKey(w => w.IdWidget);
                e.Property(w => w.Nombre).HasMaxLength(100).IsRequired();
                e.Property(w => w.Tipo).HasMaxLength(50).IsRequired();
                e.Property(w => w.UrlApi).HasMaxLength(255);
                e.Property(w => w.ApiKey).HasMaxLength(255);
                e.Property(w => w.RutaImagen).HasMaxLength(255);
               
            });

           
            modelBuilder.Entity<ConfiguracionUsuario>(e =>
            {
                e.ToTable("ConfiguracionUsuario");
                e.HasKey(c => c.IdConfiguracion);
                e.Property(c => c.Posicion).HasDefaultValue(0);
                e.Property(c => c.Favorito).HasDefaultValue(false);
                e.Property(c => c.Visible).HasDefaultValue(true);
            });
        }
    }
}
