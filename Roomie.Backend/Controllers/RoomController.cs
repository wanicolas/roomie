using Microsoft.AspNetCore.Mvc;
using Roomie.Backend.Data;
using Roomie.Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Annotations;

namespace Roomie.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RoomController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoomController(
            AppDbContext context,
            ILogger<RoomController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Récupère les salles en fonction de critères de recherche")]
        [SwaggerResponse(200, "Liste des salles disponibles", typeof(IEnumerable<Room>))]
        [SwaggerResponse(404, "Aucune salle trouvée")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms(
            [FromQuery] int? capacity,
            [FromQuery] string? availableDate,
            [FromQuery] string? availableStartTime,
            [FromQuery] string? availableEndTime,
            [FromQuery] int? surface,
            [FromQuery] bool? accessiblePMR,
            [FromQuery] string? equipments,
            [FromQuery] int? minSeatingCapacity)
        {
            var query = _context.Rooms.AsQueryable();

            if (capacity.HasValue)
                query = query.Where(r => r.Capacity >= capacity.Value);

            if (DateTime.TryParse(availableDate, out var parsedDate))
				query = query.Where(r => r.AvailableDate == DateOnly.FromDateTime(parsedDate));

			if (TimeOnly.TryParse(availableStartTime, out var parsedStartTime))
				query = query.Where(r => r.AvailableStartTime <= parsedStartTime);

			if (TimeOnly.TryParse(availableEndTime, out var parsedEndTime))
				query = query.Where(r => r.AvailableEndTime >= parsedEndTime);
				
            if (surface.HasValue)
                query = query.Where(r => r.Surface >= surface.Value);

            if (accessiblePMR.HasValue)
                query = query.Where(r => r.AccessiblePMR == accessiblePMR.Value);

            if (!string.IsNullOrEmpty(equipments))
            {
                var equipmentList = equipments.Split(',').Select(e => e.Trim()).ToList();
                query = query.Where(r => equipmentList.All(eq => r.Equipments.Contains(eq)));
            }

            if (minSeatingCapacity.HasValue)
                query = query.Where(r => r.MinSeatingCapacity >= minSeatingCapacity.Value);

            var rooms = await query.ToListAsync();

            if (!rooms.Any())
                return NotFound("Aucune salle ne correspond aux critères.");

            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return NotFound($"Aucune salle trouvée avec l'ID {id}.");
            return Ok(room);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return NotFound();

            room.Name = updatedRoom.Name;
            room.Capacity = updatedRoom.Capacity;
            room.Surface = updatedRoom.Surface;
            room.AccessiblePMR = updatedRoom.AccessiblePMR;
            room.Equipments = updatedRoom.Equipments;
            room.MinSeatingCapacity = updatedRoom.MinSeatingCapacity;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                var room = await _context.Rooms.FindAsync(id);
                if (room == null)
                    return NotFound(new { message = "Salle non trouvée" });

                var hasReservations = await _context.Reservations
                    .AnyAsync(r => r.RoomId == id);

                if (hasReservations)
                    return BadRequest(new { message = "Impossible de supprimer une salle avec des réservations" });

                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Salle supprimée avec succès" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression de la salle");
                return StatusCode(500, new { message = "Erreur interne du serveur", details = ex.Message });
            }
        }
    }
}