using Roomie.Backend.Models;

namespace Roomie.Backend.Data
{
    public static class RoomSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Rooms.Any()) // Vérifie si la table est vide
            {
                var rooms = new List<Room>
                {
                    new Room
                    {
                        Name = "Salle Alpha",
                        Capacity = 20,
                        Surface = 50.5,
                        AccessiblePMR = true,
                        Equipments = "Vidéoprojecteur,Enceintes",
                        MinSeatingCapacity = 10,
                        ImageUrl = "https://source.unsplash.com/random/300x200?room"
                    },
                    new Room
                    {
                        Name = "Salle Beta",
                        Capacity = 50,
                        Surface = 100.0,
                        AccessiblePMR = false,
                        Equipments = "Enceintes",
                        MinSeatingCapacity = 25,
                        ImageUrl = "https://source.unsplash.com/random/300x200?conference"
                    }
                };

                context.Rooms.AddRange(rooms);
                context.SaveChanges();
            }
        }
    }
}
