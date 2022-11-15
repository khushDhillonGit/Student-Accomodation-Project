using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentAccomodation.Data.Migrations
{
    public partial class updatedStudentAndHouseInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "studentNumber",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Occupancy",
                table: "Houses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerPhone",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "studentNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Occupancy",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "OwnerPhone",
                table: "Houses");
        }
    }
}
