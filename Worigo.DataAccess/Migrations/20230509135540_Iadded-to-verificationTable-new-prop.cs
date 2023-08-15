using Microsoft.EntityFrameworkCore.Migrations;

namespace Worigo.DataAccess.Migrations
{
    public partial class IaddedtoverificationTablenewprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VerificationId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationId",
                table: "Customers");
        }
    }
}
