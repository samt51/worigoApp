using Microsoft.EntityFrameworkCore.Migrations;

namespace Worigo.DataAccess.Migrations
{
    public partial class addnewş : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFull",
                table: "vertificationCodes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}
