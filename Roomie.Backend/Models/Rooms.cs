namespace Roomie.Backend.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public double Surface { get; set; }
        public bool AccessiblePMR { get; set; }
        public string Equipments { get; set; } = string.Empty; // Chaîne séparée par des virgules
        public int MinSeatingCapacity { get; set; }
        public string? ImageUrl { get; set; }  // Nouvelle propriété

        public List<Booking> Bookings { get; set; } = new List<Booking>(); // Liste des réservations

        public List<string> GetEquipmentsList()
        {
            return Equipments.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public void SetEquipmentsList(List<string> equipments)
        {
            Equipments = string.Join(",", equipments);
        }
    }
}
