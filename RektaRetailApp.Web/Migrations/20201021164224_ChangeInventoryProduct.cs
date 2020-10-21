using Microsoft.EntityFrameworkCore.Migrations;

namespace RektaRetailApp.Web.Migrations
{
    public partial class ChangeInventoryProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Products_ItemId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_ItemId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Inventories");

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_InventoryId",
                table: "Products",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Inventories_InventoryId",
                table: "Products",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Inventories_InventoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_InventoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Inventories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ItemId",
                table: "Inventories",
                column: "ItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Products_ItemId",
                table: "Inventories",
                column: "ItemId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
