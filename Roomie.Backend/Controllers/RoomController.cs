using Microsoft.AspNetCore.Mvc;
using Roomie.Backend.Data;
using Roomie.Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace Roomie.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly AppDbContext _context;  // Utiliser AppDbContext

        public RoomController(AppDbContext context)  // Utiliser AppDbContext
        {
            _context = context;
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
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return NotFound();

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
    
}
