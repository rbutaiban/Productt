using Microsoft.EntityFrameworkCore;
using Product.Models;

namespace Product.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Productt> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
