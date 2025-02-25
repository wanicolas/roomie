using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                columns: new[] { "Id", "AccessiblePMR", "AvailableDate", "AvailableEndTime", "AvailableStartTime", "Capacity", "ImageUrl", "MinSeatingCapacity", "Name", "Surface" },
                values: new object[,]
                {
                    { 1, true, new DateOnly(1, 1, 1), new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0), "", 20, "Vidéoprojecteur, Enceintes", null, 0, "Salle Alpha", 0.0 },
                    { 2, false, new DateOnly(1, 1, 1), new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0), "", 50, "Vidéoprojecteur", null, 0, "Salle Beta", 0.0 },
                    { 3, true, new DateOnly(1, 1, 1), new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0), "", 10, "Enceintes", null, 0, "Salle Gamma", 0.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
