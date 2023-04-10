using GaleriaImagenes.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleriaImagenes.MyContext
{
    public class AppDbContext : DbContext
    {
        private const string connection = @"Database=image_gallery;Data Source=127.0.0.1;User Id=root;Password=Valentina24-25";

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
                optionsBuilder.UseMySQL(connection);
            }
        }

        public DbSet<Image_gallery> Image_gallery { get; set; }
    }
}
