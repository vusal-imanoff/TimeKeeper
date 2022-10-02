using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeKeeperFinal.Data.Migrations
{
    public partial class UpdateAppUserTableAddedIsDeActiveColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeActive",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeActive",
                table: "AspNetUsers");
        }
    }
}
