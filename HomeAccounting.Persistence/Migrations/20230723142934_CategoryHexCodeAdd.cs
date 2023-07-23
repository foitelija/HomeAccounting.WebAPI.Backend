using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeAccounting.Persistence.Migrations
{
    public partial class CategoryHexCodeAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorHexCode",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorHexCode",
                table: "Categories");
        }
    }
}
