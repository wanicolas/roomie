using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roomie.Backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRoomsTable : Migration
    {
        /// <inheritdoc />
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.DropTable("Rooms");
}

        /// <inheritdoc />
protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.DropTable("Rooms");
}
    }
}
