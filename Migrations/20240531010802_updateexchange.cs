using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FBC.Migrations
{
    /// <inheritdoc />
    public partial class updateexchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "562bda45-fbe3-405c-bab0-49931aa75d20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f81ad6b8-f3e2-4949-8e75-dfdbc8724c0c");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CompleteDate",
                table: "ExchangeRequest",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreateDate",
                table: "ExchangeRequest",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "ExchangeRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8c6a8be9-53c6-48e4-ad21-35161858a2bb", null, "client", "client" },
                    { "faef8332-2c9f-4bb9-bdfa-45726f7f19ef", null, "admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c6a8be9-53c6-48e4-ad21-35161858a2bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "faef8332-2c9f-4bb9-bdfa-45726f7f19ef");

            migrationBuilder.DropColumn(
                name: "CompleteDate",
                table: "ExchangeRequest");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ExchangeRequest");

            migrationBuilder.DropColumn(
                name: "Response",
                table: "ExchangeRequest");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "562bda45-fbe3-405c-bab0-49931aa75d20", null, "admin", "admin" },
                    { "f81ad6b8-f3e2-4949-8e75-dfdbc8724c0c", null, "client", "client" }
                });
        }
    }
}
