using Microsoft.EntityFrameworkCore.Migrations;

namespace RektaRetailApp.Web.Migrations
{
    public partial class SupplierRelatedChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Suppliers",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Suppliers");
        }
    }
}
