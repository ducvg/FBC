using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FBC.Migrations
{
    /// <inheritdoc />
    public partial class InitWebDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08cd3320-35a7-45ca-98be-e5ef3e1161bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b8d1cce-96f4-4020-8ccb-83f7d88a2cef");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "534c2487-cc9f-48d6-92e9-03a730312754", null, "client", "client" },
                    { "c134b0c8-d83a-4c2f-bd49-2f16bd3f8e47", null, "admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "534c2487-cc9f-48d6-92e9-03a730312754");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c134b0c8-d83a-4c2f-bd49-2f16bd3f8e47");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "08cd3320-35a7-45ca-98be-e5ef3e1161bd", null, "client", "client" },
                    { "5b8d1cce-96f4-4020-8ccb-83f7d88a2cef", null, "admin", "admin" }
                });
        }
    }
}
