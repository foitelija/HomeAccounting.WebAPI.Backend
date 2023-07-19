using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeAccounting.Persistence.Migrations
{
    public partial class SomeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FamilyMemberId",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_FamilyMemberId",
                table: "PurchaseOrders",
                column: "FamilyMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_FamilyMembers_FamilyMemberId",
                table: "PurchaseOrders",
                column: "FamilyMemberId",
                principalTable: "FamilyMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_FamilyMembers_FamilyMemberId",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_FamilyMemberId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "FamilyMemberId",
                table: "PurchaseOrders");
        }
    }
}
