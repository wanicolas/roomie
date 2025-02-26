using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Roomie.Backend.Data;
using Roomie.Backend.Models;
using System.Globalization;
using System.Security.Claims;
using System.Text;

namespace Roomie.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Seuls les utilisateurs connectés peuvent réserver
    public class ReservationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] Reservation reservation)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("Utilisateur non authentifié.");

            reservation.UserId = userId;

            // Vérifier si la salle est dispo
            bool isRoomAvailable = !await _context.Reservations.AnyAsync(r =>
                r.RoomId == reservation.RoomId &&
                ((reservation.StartTime >= r.StartTime && reservation.StartTime < r.EndTime) ||
                 (reservation.EndTime > r.StartTime && reservation.EndTime <= r.EndTime))
            );

            if (!isRoomAvailable)
                return BadRequest("Cette salle est déjà réservée à ces horaires.");

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservation(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserReservations()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("Utilisateur non authentifié.");

            var reservations = await _context.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.Room)
                .ToListAsync();

            return Ok(reservations);
        }

        [HttpGet("admin/all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.User)
                .OrderBy(r => r.StartTime)
                .ToListAsync();

            return Ok(reservations);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                return NotFound();

            // Vérifier si l'utilisateur est admin ou propriétaire de la réservation
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && reservation.UserId != userId)
                return Forbid();

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("export-csv")]
        public async Task<IActionResult> ExportReservations()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("Utilisateur non authentifié.");

            var reservations = await _context.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.Room)
                .ToListAsync();

            if (!reservations.Any())
                return NotFound("Aucune réservation trouvée pour cet utilisateur.");

            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Id;Salle;Début;Fin");

            foreach (var reservation in reservations)
            {
                csvBuilder.AppendLine($"{reservation.Id};{reservation.Room?.Id};{reservation.StartTime:yyyy-MM-dd HH:mm};{reservation.EndTime:yyyy-MM-dd HH:mm}");
            }

            var bytes = Encoding.UTF8.GetBytes("\uFEFF" + csvBuilder.ToString()); // Ajout du BOM UTF-8
            return File(bytes, "text/csv", "reservations.csv");
        }
        [HttpGet("export-cal")]
        public async Task<IActionResult> ExportReservationsAsICal()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("Utilisateur non authentifié.");

            var reservations = await _context.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.Room)
                .ToListAsync();

            if (!reservations.Any())
                return NotFound("Aucune réservation trouvée pour cet utilisateur.");

            var sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("PRODID:-//Roomie//Reservation Calendar//FR");

            foreach (var reservation in reservations)
            {
                var startTime = reservation.StartTime.ToUniversalTime().ToString("yyyyMMddTHHmmssZ");
                var endTime = reservation.EndTime.ToUniversalTime().ToString("yyyyMMddTHHmmssZ");
                var roomName = reservation.Room?.Id.ToString() ?? "Salle inconnue"; // Remplace `Id` par `Name` si dispo

                sb.AppendLine("BEGIN:VEVENT");
                sb.AppendLine($"UID:{reservation.Id}@roomie.com");
                sb.AppendLine($"DTSTAMP:{DateTime.UtcNow:yyyyMMddTHHmmssZ}");
                sb.AppendLine($"DTSTART:{startTime}");
                sb.AppendLine($"DTEND:{endTime}");
                sb.AppendLine($"SUMMARY:Réservation - {roomName}");
                sb.AppendLine("DESCRIPTION:Réservation de salle via Roomie");
                sb.AppendLine("END:VEVENT");
            }

            sb.AppendLine("END:VCALENDAR");

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/calendar", "reservations.ics");
        }

    }
}
