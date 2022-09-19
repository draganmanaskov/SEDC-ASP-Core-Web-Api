using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEDC.Loto3000App.DataAccess.Migrations
{
    public partial class AddPrizeCollumnAndSomeStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prize",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prize",
                table: "Tickets");
        }
    }
}
