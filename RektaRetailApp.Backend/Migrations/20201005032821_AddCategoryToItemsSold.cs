using Microsoft.EntityFrameworkCore.Migrations;

namespace RektaRetailApp.Backend.Migrations
{
    public partial class AddCategoryToItemsSold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ItemSoldCategoryId",
                table: "ItemsSold",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ItemsSold_ItemSoldCategoryId",
                table: "ItemsSold",
                column: "ItemSoldCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsSold_Categories_ItemSoldCategoryId",
                table: "ItemsSold",
                column: "ItemSoldCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsSold_Categories_ItemSoldCategoryId",
                table: "ItemsSold");

            migrationBuilder.DropIndex(
                name: "IX_ItemsSold_ItemSoldCategoryId",
                table: "ItemsSold");

            migrationBuilder.DropColumn(
                name: "ItemSoldCategoryId",
                table: "ItemsSold");
        }
    }
}
