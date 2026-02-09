using Microsoft.EntityFrameworkCore;
using MobilApplikation.Models;

namespace MobilApplikation.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Concert> Concerts { get; set; } = null!;
        public DbSet<Performance> Performances { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
    }
}
