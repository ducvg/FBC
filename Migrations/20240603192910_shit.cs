using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBC.Migrations
{
    /// <inheritdoc />
    public partial class shit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WalletOrder",
                columns: table => new
                {
                    WalletOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAcountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(30,5)", nullable: true, defaultValue: 0m),
                    Credit = table.Column<decimal>(type: "decimal(30,5)", nullable: true, defaultValue: 0m),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Wallet__FHFJEW947EFF", x => x.WalletOrderId);
                    table.ForeignKey(
                        name: "FK__Wallet__UserId__JFAIEJF03855JF",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__WalletOrder__8VSDVSDVA",
                table: "WalletOrder");

            migrationBuilder.DropIndex(
                name: "IX_WalletOrder_Id",
                table: "WalletOrder");

            migrationBuilder.DropColumn(
                name: "WalletOrderId",
                table: "WalletOrder");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "WalletOrder");

            migrationBuilder.DropColumn(
                name: "BankAcountName",
                table: "WalletOrder");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "WalletOrder");

            migrationBuilder.DropColumn(
                name: "Credit",
                table: "WalletOrder");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "WalletOrder");

            migrationBuilder.DropColumn(
                name: "PaymentCode",
                table: "WalletOrder");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "WalletOrder",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
