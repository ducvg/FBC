using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FBC.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4e6ee85-5d9a-416a-8f1e-2987daffd2d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4284eb6-e680-42ec-8d7a-0405b8ae257b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a62894a-0a28-44cc-8bff-0ef04e0e3ae1", null, "client", "client" },
                    { "d419b02f-ce4c-43f0-ad05-158ff3a1db3c", null, "admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a62894a-0a28-44cc-8bff-0ef04e0e3ae1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d419b02f-ce4c-43f0-ad05-158ff3a1db3c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b4e6ee85-5d9a-416a-8f1e-2987daffd2d1", null, "admin", "admin" },
                    { "f4284eb6-e680-42ec-8d7a-0405b8ae257b", null, "client", "client" }
                });
        }
    }
}
