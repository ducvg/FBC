using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FBC.Migrations
{
    /// <inheritdoc />
    public partial class NameMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8238b8a8-0382-4229-9f23-b07be4a55c9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b123f6c4-042d-48f4-9239-2d640158bf95");

            migrationBuilder.AlterColumn<string>(
                name: "Condition",
                table: "ExchangeRequest",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4aa0b4a0-829d-4523-bda7-94e2e13b7861", null, "admin", "admin" },
                    { "d40e4469-1705-4ca6-82ff-06a049992ad7", null, "client", "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4aa0b4a0-829d-4523-bda7-94e2e13b7861");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d40e4469-1705-4ca6-82ff-06a049992ad7");

            migrationBuilder.AlterColumn<int>(
                name: "Condition",
                table: "ExchangeRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8238b8a8-0382-4229-9f23-b07be4a55c9e", null, "admin", "admin" },
                    { "b123f6c4-042d-48f4-9239-2d640158bf95", null, "client", "client" }
                });
        }
    }
}
