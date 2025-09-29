using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultApiKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApiKeys",
                columns: new[] { "Id", "AppName", "CreatedAtUtc", "ExpiresAtUtc", "Key" },
                values: new object[] { 1, "app", new DateTimeOffset(new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -8, 0, 0, 0)), null, "dXNlcjFAbG9jYWxob3N0LmNvbTpQQHNzd29yZDE=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApiKeys",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
