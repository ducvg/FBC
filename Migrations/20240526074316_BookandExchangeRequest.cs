using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FBC.Migrations
{
    /// <inheritdoc />
    public partial class BookandExchangeRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04c451db-3ed9-49c0-a6cb-62b89f6bc8c6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cc7265f-795c-4895-b319-6846ce38f791");

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "ExchangeRequest",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddColumn<int>(
                name: "ConditionNumeric",
                table: "Book",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8b96a2ed-9f9b-4c7c-97c7-749599e153ec", null, "admin", "admin" },
                    { "9af40ede-6a58-4cca-a328-a341184f2edf", null, "client", "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b96a2ed-9f9b-4c7c-97c7-749599e153ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9af40ede-6a58-4cca-a328-a341184f2edf");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "ExchangeRequest");

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

            migrationBuilder.DropColumn(
                name: "ConditionNumeric",
                table: "Book");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04c451db-3ed9-49c0-a6cb-62b89f6bc8c6", null, "admin", "admin" },
                    { "8cc7265f-795c-4895-b319-6846ce38f791", null, "client", "client" }
                });
        }
    }
}
