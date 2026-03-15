using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class add_seeded_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2026, 3, 12, 12, 27, 15, 45, DateTimeKind.Local).AddTicks(4960), "This is the Royal Villa", "https://dotnetmastery.com/bluevillaimages/villa3.jpg", "Royal Villa", 4, 200.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "", new DateTime(2026, 3, 12, 12, 27, 15, 45, DateTimeKind.Local).AddTicks(5005), "This is the Premium Pool Villa", "https://dotnetmastery.com/bluevillaimages/villa1.jpg", "Premium Pool Villa", 4, 300.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "", new DateTime(2026, 3, 12, 12, 27, 15, 45, DateTimeKind.Local).AddTicks(5007), "This is 3 Villa", "https://dotnetmastery.com/bluevillaimages/villa2.jpg", "Villa 3", 4, 400.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "", new DateTime(2026, 3, 12, 12, 27, 15, 45, DateTimeKind.Local).AddTicks(5010), "This is 4 Villa", "https://dotnetmastery.com/bluevillaimages/villa4.jpg", "Villa 4", 4, 500.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "", new DateTime(2026, 3, 12, 12, 27, 15, 45, DateTimeKind.Local).AddTicks(5012), "This is 5 Villa", "https://dotnetmastery.com/bluevillaimages/villa5.jpg", "Villa 5", 4, 600.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
