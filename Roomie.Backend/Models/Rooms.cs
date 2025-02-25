namespace Roomie.Backend.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public double Surface { get; set; }
        public bool AccessiblePMR { get; set; }
        public int MinSeatingCapacity { get; set; }
        public string? ImageUrl { get; set; }  // Nouvelle propriété
        public bool HasProjector { get; set; }
        public bool HasSpeakers { get; set; }

        public List<Booking> Bookings { get; set; } = new List<Booking>(); // Liste des réservations

    }
}
