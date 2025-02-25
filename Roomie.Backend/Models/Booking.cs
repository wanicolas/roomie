namespace Roomie.Backend.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }  // Référence à la salle réservée
        public Room Room { get; set; } = null!; // Relation avec la salle

        public DateTime StartTime { get; set; } // Date et heure de début
        public DateTime EndTime { get; set; }   // Date et heure de fin

        public string UserId { get; set; } = string.Empty; // Qui a réservé
    }
}
