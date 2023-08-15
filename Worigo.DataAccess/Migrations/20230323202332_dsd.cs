using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Worigo.DataAccess.Migrations
{
    public partial class dsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceOfHotel");

            migrationBuilder.DropTable(
                name: "ServiceValueOfEmployeeType");

            migrationBuilder.DropTable(
                name: "ServiceValueOfHotel");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ServicesValues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "isProccessingDate",
                table: "OrderList",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "finishDate",
                table: "OrderList",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "serviceValueId",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServiceOfValues",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ServiceValueId = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOfValues", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceOfValues");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ServicesValues");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "finishDate",
                table: "OrderList");

            migrationBuilder.DropColumn(
                name: "serviceValueId",
                table: "Comment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "isProccessingDate",
                table: "OrderList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceOfHotel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    hotelid = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    serviceid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOfHotel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceValueOfEmployeeType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    descrptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employeetypeid = table.Column<int>(type: "int", nullable: false),
                    hotelid = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    serviceid = table.Column<int>(type: "int", nullable: false),
                    servicevalueid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceValueOfEmployeeType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceValueOfHotel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hotelid = table.Column<int>(type: "int", nullable: false),
                    serviceid = table.Column<int>(type: "int", nullable: false),
                    valueid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceValueOfHotel", x => x.id);
                });
        }
    }
}
