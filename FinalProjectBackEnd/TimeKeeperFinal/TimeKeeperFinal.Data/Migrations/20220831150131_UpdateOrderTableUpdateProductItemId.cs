using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeKeeperFinal.Data.Migrations
{
    public partial class UpdateOrderTableUpdateProductItemId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "ProductItemId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductItemId",
                table: "Orders",
                column: "ProductItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ProductItems_ProductItemId",
                table: "Orders",
                column: "ProductItemId",
                principalTable: "ProductItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ProductItems_ProductItemId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductItemId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductItemId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
