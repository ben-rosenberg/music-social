using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicSocial.Migrations
{
    public partial class ChangeAlbumRatingName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumRating",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "AlbumRatingNumber",
                table: "Posts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumRatingNumber",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "AlbumRating",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
