using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentAccomodation.Data.Migrations
{
    public partial class ImageInHomeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Houses");
        }
    }
}
