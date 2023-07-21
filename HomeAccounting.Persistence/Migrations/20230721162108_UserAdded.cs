using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeAccounting.Persistence.Migrations
{
    public partial class IsMarriedToUserAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "FamilyMembers");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "FamilyMembers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "FamilyMembers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "FamilyMembers");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "FamilyMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
