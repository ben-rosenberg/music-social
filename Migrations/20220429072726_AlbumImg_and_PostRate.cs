using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicSocial.Migrations
{
    public partial class AlbumImg_and_PostRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlbumRating",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Albums",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumRating",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Albums");
        }
    }
}
