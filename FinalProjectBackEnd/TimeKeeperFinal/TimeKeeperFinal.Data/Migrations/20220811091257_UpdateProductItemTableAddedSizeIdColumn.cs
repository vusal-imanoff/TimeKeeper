using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeKeeperFinal.Data.Migrations
{
    public partial class UpdateProductItemTableAddedSizeIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "ProductItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_SizeId",
                table: "ProductItems",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_Sizes_SizeId",
                table: "ProductItems",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_Sizes_SizeId",
                table: "ProductItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_SizeId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "ProductItems");
        }
    }
}
