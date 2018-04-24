using Microsoft.EntityFrameworkCore;

namespace bright.Models
{
    public class BrightContext : DbContext
    {
        public BrightContext (DbContextOptions<BrightContext> options) : base(options){}
        public DbSet<User> users { get; set; }
        public DbSet<Bright> posts { get; set; }
        public DbSet<Like> likes { get; set; }
    }
}