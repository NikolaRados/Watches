using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Watches.DataAccess.Migrations
{
    public partial class promena_use_case2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserUseCase");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "UserUseCase");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserUseCase");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserUseCase");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "UserUseCase");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserUseCase",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "UserUseCase",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserUseCase",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserUseCase",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "UserUseCase",
                type: "datetime2",
                nullable: true);
        }
    }
}
