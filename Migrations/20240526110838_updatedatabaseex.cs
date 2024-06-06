using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FBC.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabaseex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a62894a-0a28-44cc-8bff-0ef04e0e3ae1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d419b02f-ce4c-43f0-ad05-158ff3a1db3c");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "CartOrder");

            migrationBuilder.DropColumn(
                name: "Condition",
                table: "CartOrder");

            migrationBuilder.DropColumn(
                name: "Credit",
                table: "CartOrder");

            migrationBuilder.DropColumn(
                name: "Image1",
                table: "CartOrder");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "CartOrder");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "769766c8-0221-4b0a-bf3b-13036fa61ba4", null, "client", "client" },
                    { "f6099431-4f72-4bf5-91b0-564ebb2bb00f", null, "admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "769766c8-0221-4b0a-bf3b-13036fa61ba4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6099431-4f72-4bf5-91b0-564ebb2bb00f");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "CartOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "CartOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Credit",
                table: "CartOrder",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "CartOrder",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "CartOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a62894a-0a28-44cc-8bff-0ef04e0e3ae1", null, "client", "client" },
                    { "d419b02f-ce4c-43f0-ad05-158ff3a1db3c", null, "admin", "admin" }
                });
        }
    }
}
