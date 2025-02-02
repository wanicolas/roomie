using Microsoft.EntityFrameworkCore;
using Roomie.Backend.Models;

namespace Roomie.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Room> Rooms { get; set; }
    }
}