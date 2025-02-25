using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Roomie.Backend.Models;

namespace Roomie.Backend.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ajout des utilisateurs pour éviter l'erreur de clé étrangère
            var user1 = new ApplicationUser 
            { 
                Id = "user1", 
                UserName = "user1@example.com", 
                Email = "user1@example.com", 
                NormalizedUserName = "USER1@EXAMPLE.COM", 
                NormalizedEmail = "USER1@EXAMPLE.COM" 
            };

            var user2 = new ApplicationUser 
            { 
                Id = "user2", 
                UserName = "user2@example.com", 
                Email = "user2@example.com", 
                NormalizedUserName = "USER2@EXAMPLE.COM", 
                NormalizedEmail = "USER2@EXAMPLE.COM" 
            };

            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
            user1.PasswordHash = hasher.HashPassword(user1, "Password123!");
            user2.PasswordHash = hasher.HashPassword(user2, "Password123!");

            modelBuilder.Entity<ApplicationUser>().HasData(user1, user2);

            // Seed des salles
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Salle Alpha", Capacity = 20, AccessiblePMR = true },
                new Room { Id = 2, Name = "Salle Beta", Capacity = 50, AccessiblePMR = false },
                new Room { Id = 3, Name = "Salle Gamma", Capacity = 10, AccessiblePMR = true }
            );

            // Seed des réservations après l'ajout des utilisateurs
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { Id = 1, UserId = "user1", RoomId = 1, StartTime = DateTime.Now.AddHours(2), EndTime = DateTime.Now.AddHours(4) },
                new Reservation { Id = 2, UserId = "user2", RoomId = 2, StartTime = DateTime.Now.AddDays(1), EndTime = DateTime.Now.AddDays(1).AddHours(3) }
            );
        }
    }
}
