using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Roomie.Backend.Models;

namespace Roomie.Backend.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
		public DbSet<Booking> Bookings { get; set; }        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed des salles
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Salle Alpha", Capacity = 20, AccessiblePMR = true },
                new Room { Id = 2, Name = "Salle Beta", Capacity = 50, AccessiblePMR = false },
                new Room { Id = 3, Name = "Salle Gamma", Capacity = 10, AccessiblePMR = true }
            );
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { Id = 1, UserId = "user1", RoomId = 1, StartTime = DateTime.Now.AddHours(2), EndTime = DateTime.Now.AddHours(4) },
                new Reservation { Id = 2, UserId = "user2", RoomId = 2, StartTime = DateTime.Now.AddDays(1), EndTime = DateTime.Now.AddDays(1).AddHours(3) }
            );
        }
    }
}
