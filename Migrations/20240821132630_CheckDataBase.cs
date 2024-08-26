using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luce.Migrations
{
    public partial class CheckDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CartItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CartItems",
                type: "longtext",
                nullable: true);
        }
    }
}
