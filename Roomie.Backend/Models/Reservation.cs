namespace Roomie.Backend.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; } // Clé étrangère vers Room
        public Room Room { get; set; } = null!; // Propriété de navigation

        public string UserEmail { get; set; } = string.Empty; // Ex: email de l'utilisateur qui réserve
        public DateTime StartTime { get; set; } // Début de la réservation
        public DateTime EndTime { get; set; } // Fin de la réservation

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = default!;
    }
}
