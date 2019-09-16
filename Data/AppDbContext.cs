using _50Pixels.Models;
using Microsoft.EntityFrameworkCore;

namespace _50Pixels.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Photo> Photos { get; set; }
    }
}