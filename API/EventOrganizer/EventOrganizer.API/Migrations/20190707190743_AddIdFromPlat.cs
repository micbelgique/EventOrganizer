using Microsoft.EntityFrameworkCore.Migrations;

namespace EventOrganizer.API.Migrations
{
    public partial class AddIdFromPlat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdFromPlat",
                table: "UserPictures",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdFromPlat",
                table: "Pictures",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdFromPlat",
                table: "UserPictures");

            migrationBuilder.DropColumn(
                name: "IdFromPlat",
                table: "Pictures");
        }
    }
}
