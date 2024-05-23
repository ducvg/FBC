using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FBC.Migrations
{
    /// <inheritdoc />
    public partial class updateRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "CategoryExchangeRequest",
                columns: table => new
                {
                    CategoriesCategoryId = table.Column<int>(type: "int", nullable: false),
                    ExchangeRequestsExchangeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryExchangeRequest", x => new { x.CategoriesCategoryId, x.ExchangeRequestsExchangeId });
                    table.ForeignKey(
                        name: "FK_CategoryExchangeRequest_Category_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryExchangeRequest_ExchangeRequest_ExchangeRequestsExchangeId",
                        column: x => x.ExchangeRequestsExchangeId,
                        principalTable: "ExchangeRequest",
                        principalColumn: "ExchangeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04c451db-3ed9-49c0-a6cb-62b89f6bc8c6", null, "admin", "admin" },
                    { "8cc7265f-795c-4895-b319-6846ce38f791", null, "client", "client" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryExchangeRequest_ExchangeRequestsExchangeId",
                table: "CategoryExchangeRequest",
                column: "ExchangeRequestsExchangeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryExchangeRequest");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04c451db-3ed9-49c0-a6cb-62b89f6bc8c6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cc7265f-795c-4895-b319-6846ce38f791");

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
    }
}
