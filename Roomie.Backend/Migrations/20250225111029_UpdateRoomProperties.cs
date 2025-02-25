using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Roomie.Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoomProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableDate",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "AvailableEndTime",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "AvailableStartTime",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Equipments",
                table: "Rooms");

            migrationBuilder.AddColumn<bool>(
                name: "HasProjector",
                table: "Rooms",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSpeakers",
                table: "Rooms",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "EndTime", "RoomId", "StartTime", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 25, 16, 10, 29, 128, DateTimeKind.Local).AddTicks(6310), 1, new DateTime(2025, 2, 25, 14, 10, 29, 128, DateTimeKind.Local).AddTicks(6240), "user1" },
                    { 2, new DateTime(2025, 2, 26, 15, 10, 29, 128, DateTimeKind.Local).AddTicks(6310), 2, new DateTime(2025, 2, 26, 12, 10, 29, 128, DateTimeKind.Local).AddTicks(6310), "user2" }
                });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HasProjector", "HasSpeakers" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HasProjector", "HasSpeakers" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HasProjector", "HasSpeakers" },
                values: new object[] { false, false });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "HasProjector",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "HasSpeakers",
                table: "Rooms");

            migrationBuilder.AddColumn<DateOnly>(
                name: "AvailableDate",
                table: "Rooms",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "AvailableEndTime",
                table: "Rooms",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "AvailableStartTime",
                table: "Rooms",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Equipments",
                table: "Rooms",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AvailableDate", "AvailableEndTime", "AvailableStartTime", "Equipments" },
                values: new object[] { new DateOnly(1, 1, 1), new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0), "Vidéoprojecteur, Enceintes" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AvailableDate", "AvailableEndTime", "AvailableStartTime", "Equipments" },
                values: new object[] { new DateOnly(1, 1, 1), new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0), "Vidéoprojecteur" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AvailableDate", "AvailableEndTime", "AvailableStartTime", "Equipments" },
                values: new object[] { new DateOnly(1, 1, 1), new TimeOnly(0, 0, 0), new TimeOnly(0, 0, 0), "Enceintes" });
        }
    }
}
