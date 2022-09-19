using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEDC.Loto3000App.DataAccess.Migrations
{
    public partial class SessionAddActiveBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Sessions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Sessions");
        }
    }
}
