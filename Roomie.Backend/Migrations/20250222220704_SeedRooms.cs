using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roomie.Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedRooms : Migration
    {
        /// <inheritdoc />
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.InsertData(
        table: "Rooms",
        columns: new[] { "Id", "Name", "Building", "Capacity", "Surface", "IsAccessiblePMR", "HasProjector", "HasSpeakers", "MinSeats" },
        values: new object[,]
        {
            { 1, "Salle Conférence A", "Bâtiment Alpha", 50, 80, true, true, true, 40 },
            { 2, "Salle Réunion B", "Bâtiment Beta", 20, 35, false, false, true, 15 },
            { 3, "Salle Polyvalente C", "Bâtiment Gamma", 100, 150, true, true, false, 80 }
        });
}

protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.DeleteData(table: "Rooms", keyColumn: "Id", keyValues: new object[] { 1, 2, 3 });
}

    }
}
