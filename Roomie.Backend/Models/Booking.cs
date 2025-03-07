namespace Roomie.Backend.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; } 
        public Room Room { get; set; } = null!; // Relation avec la salle

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}
