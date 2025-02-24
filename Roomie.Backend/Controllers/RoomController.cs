using Microsoft.AspNetCore.Mvc;
using Roomie.Backend.Data;
using Roomie.Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            if (rooms == null || !rooms.Any())
            {
                return NotFound("Aucune salle disponible.");
            }

            return Ok(rooms);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            
            if (room == null)
            {
                return NotFound($"Aucune salle trouvée avec l'ID {id}.");
            }

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

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
			Console.WriteLine("DeleteRoom");
            try
            {
                _logger.LogInformation("User Identity: {identity}", User.Identity?.Name);
                _logger.LogInformation("Is Authenticated: {auth}", User.Identity?.IsAuthenticated);
                
                foreach (var claim in User.Claims)
                {
                    _logger.LogInformation("Claim Found - Type: {type}, Value: {value}", 
                        claim.Type, claim.Value);
                }

                var isAdmin = User.Claims.Any(c => 
                    c.Type == ClaimTypes.Role && 
                    c.Value == "Admin");

                if (!isAdmin)
                {
                    _logger.LogWarning("Utilisateur non admin");
                    return Forbid();
                }

                var room = await _context.Rooms.FindAsync(id);
                if (room == null)
                {
                    return NotFound(new { message = "Salle non trouvée" });
                }

                var hasReservations = await _context.Reservations
                    .AnyAsync(r => r.RoomId == id);

                if (hasReservations)
                {
                    return BadRequest(new { message = "Impossible de supprimer une salle avec des réservations" });
                }

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
