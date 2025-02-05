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
    public bool AccessiblePMR { get; set; }
    public List<string> Equipments { get; set; } = new();
    public int MinSeatingCapacity { get; set; }
    public string? ImageUrl { get; set; }  // Nouvelle propriété
}
}
