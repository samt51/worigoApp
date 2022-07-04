using Microsoft.EntityFrameworkCore.Migrations;

namespace Worigo.DataAccess.Migrations
{
    public partial class dbworigo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomType_RoomTypeid",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_RoomTypeid",
                table: "Room");

            migrationBuilder.AddColumn<int>(
                name: "hotelid",
                table: "vertificationCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "hotelid",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "hotelid",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hotelid",
                table: "vertificationCodes");

            migrationBuilder.DropColumn(
                name: "hotelid",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "hotelid",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Room_RoomTypeid",
                table: "Room",
                column: "RoomTypeid");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomType_RoomTypeid",
                table: "Room",
                column: "RoomTypeid",
                principalTable: "RoomType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
