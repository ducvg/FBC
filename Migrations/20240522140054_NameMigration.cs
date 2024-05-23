using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FBC.Migrations
{
    /// <inheritdoc />
    public partial class NameMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "8238b8a8-0382-4229-9f23-b07be4a55c9e", null, "admin", "admin" },
                    { "b123f6c4-042d-48f4-9239-2d640158bf95", null, "client", "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8238b8a8-0382-4229-9f23-b07be4a55c9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b123f6c4-042d-48f4-9239-2d640158bf95");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "534c2487-cc9f-48d6-92e9-03a730312754", null, "client", "client" },
                    { "c134b0c8-d83a-4c2f-bd49-2f16bd3f8e47", null, "admin", "admin" }
                });
        }
    }
}
