using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luce.Migrations
{
    public partial class newmi2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Orders",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
