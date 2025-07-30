using Microsoft.EntityFrameworkCore;
using ModelHelper.Models;

namespace Proyecto_HGC_SIGEM_G6.Context
{
    public class DBContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Usuario>().ToTable("Usuarios"); 

        //}
    }

}
