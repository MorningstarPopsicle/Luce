using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luce.Migrations
{
    public partial class newmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CartItems_CartItemId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CartItemId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CartItemId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CartItemId1",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_OrderId",
                table: "CartItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Orders_OrderId",
                table: "CartItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Orders_OrderId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_OrderId",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "CartItemId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CartItemId1",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CartItemId1",
                table: "Orders",
                column: "CartItemId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CartItems_CartItemId1",
                table: "Orders",
                column: "CartItemId1",
                principalTable: "CartItems",
                principalColumn: "Id");
        }
    }
}
