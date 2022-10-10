using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentAccomodation.Data.Migrations
{
    public partial class addedrelation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Houses_HouseId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "HouseId",
                table: "Students",
                newName: "HouseID");

            migrationBuilder.RenameIndex(
                name: "IX_Students_HouseId",
                table: "Students",
                newName: "IX_Students_HouseID");

            migrationBuilder.AlterColumn<int>(
                name: "HouseID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Houses_HouseID",
                table: "Students",
                column: "HouseID",
                principalTable: "Houses",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Houses_HouseID",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "HouseID",
                table: "Students",
                newName: "HouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_HouseID",
                table: "Students",
                newName: "IX_Students_HouseId");

            migrationBuilder.AlterColumn<int>(
                name: "HouseId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Houses_HouseId",
                table: "Students",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "HouseId");
        }
    }
}
