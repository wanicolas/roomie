namespace Roomie.Backend.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Building { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public DateOnly AvailableDate { get; set; }
        public TimeOnly AvailableStartTime { get; set; }
        public TimeOnly AvailableEndTime { get; set; }
        public double Surface { get; set; }
         public bool IsAccessiblePMR { get; set; }
        public string Equipments { get; set; } = string.Empty; // Chaine séparée par des virgules
        public int MinSeatingCapacity { get; set; }
        public string? ImageUrl { get; set; }

        // Relation One-to-Many : Une salle peut avoir plusieurs réservations
        public List<Reservation> Reservations { get; set; } = new();
    }
}
