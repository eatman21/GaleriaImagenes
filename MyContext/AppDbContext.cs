using GaleriaImagenes.Entidades;
using GaleriaImagenes.Helpers;
using Microsoft.EntityFrameworkCore;

namespace GaleriaImagenes.MyContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");
                optionsBuilder.UseMySQL(connectionString);
            }
        }

        public DbSet<Image_gallery> Image_gallery { get; set; }
    }
}
