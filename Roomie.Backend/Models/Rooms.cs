namespace Roomie.Backend.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public bool AccessiblePMR { get; set; }
        public List<string> Equipments { get; set; } = new();
    }
}
