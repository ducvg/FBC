using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FBC.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryIdToExchangeRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4aa0b4a0-829d-4523-bda7-94e2e13b7861");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d40e4469-1705-4ca6-82ff-06a049992ad7");

            migrationBuilder.AddColumn<int>(
                name: "ExchangeRequestExchangeId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "65acc9bf-9e8c-4cfc-84c3-091265508eff", null, "client", "client" },
                    { "679004a7-910c-4ce6-ad72-d47b7c449906", null, "admin", "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_ExchangeRequestExchangeId",
                table: "Category",
                column: "ExchangeRequestExchangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_ExchangeRequest_ExchangeRequestExchangeId",
                table: "Category",
                column: "ExchangeRequestExchangeId",
                principalTable: "ExchangeRequest",
                principalColumn: "ExchangeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_ExchangeRequest_ExchangeRequestExchangeId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_ExchangeRequestExchangeId",
                table: "Category");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65acc9bf-9e8c-4cfc-84c3-091265508eff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "679004a7-910c-4ce6-ad72-d47b7c449906");

            migrationBuilder.DropColumn(
                name: "ExchangeRequestExchangeId",
                table: "Category");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4aa0b4a0-829d-4523-bda7-94e2e13b7861", null, "admin", "admin" },
                    { "d40e4469-1705-4ca6-82ff-06a049992ad7", null, "client", "client" }
                });
        }
    }
}
