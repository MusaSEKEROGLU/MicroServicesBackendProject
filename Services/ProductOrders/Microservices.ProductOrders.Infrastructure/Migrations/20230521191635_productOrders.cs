using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microservices.ProductOrders.Infrastructure.Migrations
{
    public partial class productOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ordering");

            migrationBuilder.CreateTable(
                name: "ProductOrders",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address_Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Line = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrderItems",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrderItems_ProductOrders_ProductOrderId",
                        column: x => x.ProductOrderId,
                        principalSchema: "ordering",
                        principalTable: "ProductOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderItems_ProductOrderId",
                schema: "ordering",
                table: "ProductOrderItems",
                column: "ProductOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOrderItems",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "ProductOrders",
                schema: "ordering");
        }
    }
}
