using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roomie.Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 2, 25, 16, 13, 45, 972, DateTimeKind.Local).AddTicks(7430), new DateTime(2025, 2, 25, 14, 13, 45, 972, DateTimeKind.Local).AddTicks(7390) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 2, 26, 15, 13, 45, 972, DateTimeKind.Local).AddTicks(7440), new DateTime(2025, 2, 26, 12, 13, 45, 972, DateTimeKind.Local).AddTicks(7430) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 2, 25, 16, 11, 35, 300, DateTimeKind.Local).AddTicks(9320), new DateTime(2025, 2, 25, 14, 11, 35, 300, DateTimeKind.Local).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 2, 26, 15, 11, 35, 300, DateTimeKind.Local).AddTicks(9330), new DateTime(2025, 2, 26, 12, 11, 35, 300, DateTimeKind.Local).AddTicks(9320) });
        }
    }
}
