using Microsoft.EntityFrameworkCore;
using FashionEcommerceAPI.Models;

namespace FashionEcommerceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Products> Products { get; set; }
    }
}
