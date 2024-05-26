using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FBC.Migrations
{
    /// <inheritdoc />
    public partial class updateER : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Category",
                table: "ExchangeRequest");

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "ExchangeRequest",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                table: "ExchangeRequest",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoPage",
                table: "ExchangeRequest",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "ExchangeRequest",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "ExchangeRequest",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b4e6ee85-5d9a-416a-8f1e-2987daffd2d1", null, "admin", "admin" },
                    { "f4284eb6-e680-42ec-8d7a-0405b8ae257b", null, "client", "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4e6ee85-5d9a-416a-8f1e-2987daffd2d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4284eb6-e680-42ec-8d7a-0405b8ae257b");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "ExchangeRequest");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "ExchangeRequest");

            migrationBuilder.DropColumn(
                name: "NoPage",
                table: "ExchangeRequest");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "ExchangeRequest");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "ExchangeRequest");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "ExchangeRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8b96a2ed-9f9b-4c7c-97c7-749599e153ec", null, "admin", "admin" },
                    { "9af40ede-6a58-4cca-a328-a341184f2edf", null, "client", "client" }
                });
        }
    }
}
